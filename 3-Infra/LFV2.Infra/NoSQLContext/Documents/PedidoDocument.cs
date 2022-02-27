using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFV2.Infra.NoSQLContext.Documents
{
    public class PedidoDocument : BaseDocument
    {
        public int idBase { get; private set; }
        public string Nome { get; private set; }
        public string Obs { get; private set; }
        private PedidoDocument() { 
        
            //caso vc precise inicializar alguma lista, etc
        }
        public PedidoDocument(Guid documentId, int idBase, string nome, string obs): this()
        {
            id = documentId;
            this.idBase = idBase;
            Nome = nome;
            Obs = obs;
        }



    }
}
