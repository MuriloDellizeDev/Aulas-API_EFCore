using Edux_Api_EFcore.Contexts;
using Edux_Api_EFcore.Domains;
using Edux_Api_EFcore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly EduxContext _ctx;

       public TurmaRepository()
        {

            _ctx = new EduxContext();

        }

        #region Leitura


        public List<Turma> Listar()
        {
            try
            {

                return _ctx.Turmas.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        public Turma BuscarPorId(Guid Id)
        {
            try
            {
                return _ctx.Turmas.Find(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Turma> BuscarPorNome(string nome)
        {
            try
            {

                return _ctx.Turmas.Where(o => o.Descricao.Contains(nome)).ToList();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        #endregion


        #region Gravação

        public Turma Adicionar(Turma turma)
        {
            try
            {

                _ctx.Turmas.Add(turma);

                _ctx.SaveChanges();

                return turma;

     

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        

        public void Editar(Turma turma)
        {
            try
            {
                Turma turmaAlterada = BuscarPorId(turma.Id);

                if (turma == null)
                    throw new Exception("Impossível alterar Turma pois faltam dados.");

                turmaAlterada.Descricao = turma.Descricao;
                turmaAlterada.IdCurso = turma.IdCurso;


                _ctx.Turmas.Update(turmaAlterada);

                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        

        public void Remover(Guid Id)
        {
            try
            {
                Turma TurmaExcluido = BuscarPorId(Id);

                if (TurmaExcluido == null)
                    throw new Exception("Impossível excluir a Turma desejado pois faltam dados.");

                _ctx.Turmas.Remove(TurmaExcluido);
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
