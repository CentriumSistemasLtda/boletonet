using System;
using System.Collections;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumInstrucoes_BancoBrasil
    {
    
    /*
        Protestar = 9,                      // Emite aviso ao sacado ap�s N dias do vencto, e envia ao cart�rio ap�s 5 dias �teis
        NaoProtestar = 10,                  // Inibe protesto, quando houver instru��o permanente na conta corrente
        ImportanciaporDiaDesconto = 30,
        Multa = 35,
        ProtestoFinsFalimentares = 42,
        ProtestarAposNDiasCorridos = 81,
        ProtestarAposNDiasUteis = 82,
        NaoReceberAposNDias = 91,
        DevolverAposNDias = 92,
        JurosdeMora = 998,
        DescontoAteDataEstipulada = 22,
        DescontoporDia = 999,
    */

        CobrarJuros = 01,
        ProtestarNo3DiaUtilAposVencido = 03,
        ProtestarNo4DiaUtilAposVencido = 04,
        ProtestarNo5DiaUtilAposVencido = 05,
        IndicaProtestoEmDiasCorridos = 06,
        NaoProtestar = 07,
        ProtestarNo10DiaCorridoAposVencido = 10,
        ProtestarNo15DiaCorridoAposVencido = 15,
        ProtestarNo20DiaCorridoAposVencido = 20,
        ConcederDescontoAteDataEstipulada = 22,
        ProtestarNo25DiaCorridoAposVencido = 25,
        ProtestarNo30DiaCorridoAposVencido = 30,
        ProtestarNo35DiaCorridoAposVencido = 35,
        ProtestarNo40DiaCorridoAposVencido = 40,
        Devolver = 42,
        Baixar = 44,
        ProtestarNo45DiaCorridoAposVencido = 45,
        EntregarAoPagadorFrancoDePagamento = 46,
        NegativacaoSemProtesto = 88
    }

    #endregion

    public class Instrucao_BancoBrasil : AbstractInstrucao, IInstrucao
    {

        #region Construtores

        public Instrucao_BancoBrasil()
        {
            try
            {
                this.Banco = new Banco(001);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public Instrucao_BancoBrasil(int codigo)
        {
            this.carregar(codigo, 0);
        }

        public Instrucao_BancoBrasil(int codigo, int nrDias)
        {
            this.carregar(codigo, nrDias);
        }

        public Instrucao_BancoBrasil(int codigo, double valor)
        {
            this.carregar(codigo, valor);
        }

        public Instrucao_BancoBrasil(int codigo, double valor, EnumTipoValor tipoValor)
        {
            this.carregar(codigo, valor, tipoValor);
        }

        #endregion

        #region Metodos Privados

        private void carregar(int idInstrucao, double valor, EnumTipoValor tipoValor = EnumTipoValor.Percentual)
        {
            try
            {
                this.Banco = new Banco_Brasil();
                this.Valida();

                switch ((EnumInstrucoes_BancoBrasil)idInstrucao)
                {
                    default:
                        this.Codigo = 0;
                        this.Descricao = " (Selecione) ";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }


        private void carregar(int idInstrucao, int nrDias)
        {
            try
            {
                this.Banco = new Banco_Brasil();
                this.Valida();

                switch ((EnumInstrucoes_BancoBrasil)idInstrucao)
                {
                    /*
                    case EnumInstrucoes_BancoBrasil.Protestar:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.Protestar;
                        this.Descricao = "Protestar ap�s " + nrDias + " dias �teis.";
                        break;
                    case EnumInstrucoes_BancoBrasil.NaoProtestar:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.NaoProtestar;
                        this.Descricao = "N�o protestar";
                        break;
                    case EnumInstrucoes_BancoBrasil.ImportanciaporDiaDesconto:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ImportanciaporDiaDesconto;
                        this.Descricao = "Import�ncia por dia de desconto.";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestoFinsFalimentares:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestoFinsFalimentares;
                        this.Descricao = "Protesto para fins falimentares";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarAposNDiasCorridos:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarAposNDiasCorridos;
                        this.Descricao = "Protestar no " + nrDias + "� dia corrido ap�s vencimento";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarAposNDiasUteis:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarAposNDiasUteis;
                        this.Descricao = "Protestar no " + nrDias + "� dia �til ap�s vencimento";//J�ferson (jefhtavares) em 02/12/2013 a pedido do setor de homologa��o do BB
                        break;
                    case EnumInstrucoes_BancoBrasil.NaoReceberAposNDias:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.NaoReceberAposNDias;
                        this.Descricao = "N�o receber ap�s " + nrDias + " dias do vencimento";
                        break;
                    case EnumInstrucoes_BancoBrasil.DevolverAposNDias:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.DevolverAposNDias;
                        this.Descricao = "Devolver ap�s " + nrDias + " dias do vencimento";
                        break;
                    case EnumInstrucoes_BancoBrasil.JurosdeMora:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.JurosdeMora;
                        this.Descricao = "Ap�s vencimento cobrar R$ "; // por dia de atraso
                        break;
                    case EnumInstrucoes_BancoBrasil.Multa:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.Multa;
                        this.Descricao = "Cobrar Multa";
                        break;
                    case EnumInstrucoes_BancoBrasil.DescontoporDia:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.DescontoporDia;
                        this.Descricao = "Conceder desconto de R$ "; // por dia de antecipa��o
                        break;
                    case EnumInstrucoes_BancoBrasil.DescontoAteDataEstipulada:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.DescontoAteDataEstipulada;
                        this.Descricao = "Conceder desconto at� a data estipulada"; // por dia de antecipa��o
                        break;
                    default:
                        this.Codigo = 0;
                        this.Descricao = "( Selecione )";
                        break;
                     */
                    case EnumInstrucoes_BancoBrasil.CobrarJuros:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.CobrarJuros;
                        this.Descricao = "Cobrar Juros";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo3DiaUtilAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo3DiaUtilAposVencido;
                        this.Descricao = "Protestar No 3 Dia �til �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo4DiaUtilAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo4DiaUtilAposVencido;
                        this.Descricao = "Protestar No 4 Dia �til �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo5DiaUtilAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo5DiaUtilAposVencido;
                        this.Descricao = "Protestar No 5 Dia �til �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.IndicaProtestoEmDiasCorridos:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.IndicaProtestoEmDiasCorridos;
                        this.Descricao = "Indica Protesto Em Dias Corridos";
                        break;
                    case EnumInstrucoes_BancoBrasil.NaoProtestar:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.NaoProtestar;
                        this.Descricao = "N�o Protestar";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo10DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo10DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 10 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo15DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo15DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 15 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo20DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo20DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 20 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ConcederDescontoAteDataEstipulada:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ConcederDescontoAteDataEstipulada;
                        this.Descricao = "ConcederDescontoAteDataEstipulada";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo25DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo25DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 25 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo30DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo30DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 30 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo35DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo35DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 35 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo40DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo40DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 40 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.Devolver:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.Devolver;
                        this.Descricao = "Devolver";
                        break;
                    case EnumInstrucoes_BancoBrasil.Baixar:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.Baixar;
                        this.Descricao = "Baixar";
                        break;
                    case EnumInstrucoes_BancoBrasil.ProtestarNo45DiaCorridoAposVencido:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.ProtestarNo45DiaCorridoAposVencido;
                        this.Descricao = "Protestar No 45 Dia Corrido �pos Vencido";
                        break;
                    case EnumInstrucoes_BancoBrasil.EntregarAoPagadorFrancoDePagamento:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.EntregarAoPagadorFrancoDePagamento;
                        this.Descricao = "Entregar Ao Pagador Franco De Pagamento";
                        break;
                    case EnumInstrucoes_BancoBrasil.NegativacaoSemProtesto:
                        this.Codigo = (int)EnumInstrucoes_BancoBrasil.NegativacaoSemProtesto;
                        this.Descricao = "Negativa��o Sem Protesto";
                        break;
                    default:
                        this.Codigo = 0;
                        this.Descricao = "( Selecione )";
                        break;

                }

                this.QuantidadeDias = nrDias;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public override void Valida()
        {
            //base.Valida();
        }

        #endregion

    }
}
