

- [x]  Importar scripts do banco
- [x]  Criar uma solução de API
- [x]  Adicionar pacotes:

    

- [x]  Criar os Domains pelo EFCore - Database First

    ```csharp
    Scaffold-DbContext "Data Source=.\SqlExpress; Initial Catalog= NyousTarde; User Id=sa; Password=sa132" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domains -ContextDir Contexts -Context NyousContext
    ```

   

    ## JWT - Autenticação da API

    - [x]  Instalar pacote JWT:

    - [x]  Adicionamos em nosso appsettings.json :

        ```csharp
        "Jwt": {
            "Key": "ThisIsMyNyousSecretKey",
            "Issuer": "nyous.com.br"
         },
        ```

    - [x]  Adicionar a configuração do nosso Serviço de autenticação:

        ```csharp
        // JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
        .AddJwtBearer(options =>  
        {  
            options.TokenValidationParameters = new TokenValidationParameters  
            {  
                ValidateIssuer = true,  
                ValidateAudience = true,  
                ValidateLifetime = true,  
                ValidateIssuerSigningKey = true,  
                ValidIssuer = Configuration["Jwt:Issuer"],  
                ValidAudience = Configuration["Jwt:Issuer"],  
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))  
            };  
        });
        ```

    - [x]  Importar as libs faltantes com Control + ponto ou:

        ```csharp
        using Microsoft.IdentityModel.Tokens;
        using Microsoft.AspNetCore.Authentication.JwtBearer;
        using System.Text;
        ```

    - [x]  Em Startup.cs , no método Configure , usamos efetivamente a autenticação:
        - **nota**: se não colocar **em cima** de *app.UseAuthorization()* , não funcionará corretamente

        ```csharp
        app.UseAuthentication();
        ```

    - [x]  Criamos o Controller *Login*:

    - [x]  Chamamos nosso contexto lá dentro:

        ```csharp
        // Chamamos nosso contexto do banco
        NyousContext _context = new NyousContext();
        ```

    - [x]  Definimos um método construtor para pegar as informações de appsettings.json:

        ```csharp
        // Definimos uma variável para percorrer nossos métodos com as configurações obtidas no appsettings.json
        private IConfiguration _config;  

        // Definimos um método construtor para poder passar essas configs
        public LoginController(IConfiguration config)  
        {  
            _config = config;  
        }
        ```

    - [x]  Criamos um método para validar nosso usuário da aplicação:

        ```csharp
        private Usuario AuthenticateUser(Usuario login)
        {
            return _context.Usuario.Include(a => a.IdAcessoNavigation).FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);
        }
        ```

    - [x]  Criamos um método que vai gerar nosso Token:

        ```csharp
        // Criamos nosso método que vai gerar nosso Token
        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        				new Claim(ClaimTypes.Role, userInfo.IdAcessoNavigation.Tipo)
            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        ```

    - [x]  Criamos o endpoint da API responsável por consumir os métodos de autenticação:

        ```csharp
        // Usamos a anotação "AllowAnonymous" para 
        // ignorar a autenticação neste método, já que é ele quem fará isso
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Usuario login)
        {
            // Definimos logo de cara como não autorizado
            IActionResult response = Unauthorized();

            // Autenticamos o usuário da API
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        ```

    - [x]  Testamos se está sendo gerado nosso Token pelo Postman, no método POST

        Pela URL :

        [https://localhost:<porta>/api/login](https://localhost:5001/api/login)

        E com os seguintes parâmetros pela RAW :

        ```csharp
        {
            "email": "paulo@senai.com.br",
            "senha": "1234567890",
        }
        ```

    - [x]  Se estiver tudo certo ele deve retornar isto no Postman:

        - [x]  Após confirmar, vamos até [https://jwt.io/](https://jwt.io/)
        - [x]  Colamos nosso Token lá e em Payload devemos ter os seguintes dados:
   

    Pronto! Agora é só utilizar a anotação em cima de cada método que desejar bloquear:

    ***[Authorize]***

    em baixo da anotação REST de cada método que desejar colocar autenticação!

    No Postman devemos gerar um token pela rota de login e nos demais endpoints devemos adicionar o token gerado na aba:

    ***Authorization***

    escolhendo a opção:

    ***Baerer Token***

    ### Testando em um Controller

    - [x]  Para testarmos, criamos o controller para "Categoria":


    ### IMPORTANTE para não gerar erros:

    - [x]  Depois de gerar a classe automaticamente trocamos este método construtor:

        ```csharp
        private readonly NyousContext _context;

        public CategoriasController(NyousContext context)
        {
            _context = context;
        }
        ```

    - [x]  Por este tipo de instância:

        ```csharp
        private NyousContext _context = new NyousContext();
        ```

    ### Continuando...

    - [x]  Testamos no postman:

    - [x]  Para testar o JWT colocamos o Authotrize nos métodos que desejamos bloquear, ou na classe toda caso ela necessite do mesmo bloqueio:

    Se rodarmos a aplicação sem nenhum token ativo, ele deve retornar erro 401 (Não autorizado):

    - [x]  Com a API rodando, vamos para o endpoint de login e geramos um novo Token

    - [x]  Copiamos o token e colamos na aba


    ### ADICIONANDO PERMISSÃO PARA TIPOS DE USUÁRIOS

    - [x]  Basta colocar as permissões, separadas por virgula:

        ```csharp
        [Authorize(Roles = "Padrao,Administrador")]
        ```

    ## Links de apoio

    [https://jwt.io/](https://jwt.io/)
