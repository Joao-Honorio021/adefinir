using System;
using System.Collections.Generic;

public class Controle_de_Turno
{
    private IEstado turnoAtual;
    private Dictionary<Type, IEstado> transicoes;

    public Controle_de_Turno()
    {
        transicoes = new Dictionary<Type, IEstado>
        {
            { typeof(EstadoPreparacao), new EstadoCombate() },
            { typeof(EstadoCombate), new EstadoVitoria() },
            { typeof(EstadoVitoria), null },  // Fim do jogo
            { typeof(EstadoDerrota), null }   // Fim do jogo
        };
        turnoAtual = new EstadoPreparacao();
    }

    public void ExecutarTurno()
    {
        turnoAtual.ExecutarAcao();
        TransitarParaProximoEstado();
    }

    private void TransitarParaProximoEstado()
    {
        var proximoEstado = transicoes[turnoAtual.GetType()];
        if (proximoEstado != null)
        {
            turnoAtual = proximoEstado;
        }
        else
        {
            Console.WriteLine("Fim do jogo.");
        }
    }

    public interface IEstado
    {
        void ExecutarAcao();
    }

    // Estado de Preparação
    public class EstadoPreparacao : IEstado
    {
        public void ExecutarAcao()
        {
            Console.WriteLine("Estado: Preparação");
            Console.WriteLine("Aguardando início do combate...");
        }
    }

    // Estado de Combate
    public class EstadoCombate : IEstado
    {
        public void ExecutarAcao()
        {
            Console.WriteLine("Estado: Combate");
            Console.WriteLine("Escolha uma ação: 1 - Atacar, 2 - Defender, 3 - Fugir");

            var escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    Console.WriteLine("Você atacou!");
                    break;
                case "2":
                    Console.WriteLine("Você se defendeu!");
                    break;
                case "3":
                    Console.WriteLine("Você fugiu do combate!");
                    break;
                default:
                    Console.WriteLine("Escolha inválida!");
                    break;
            }

            // Transita para o próximo estado
            Console.WriteLine("Você venceu o combate!");
        }
    }

    // Estado de Vitória
    public class EstadoVitoria : IEstado
    {
        public void ExecutarAcao()
        {
            Console.WriteLine("Estado: Vitória");
            Console.WriteLine("Você venceu! O jogo acabou.");
        }
    }

    // Estado de Derrota
    public class EstadoDerrota : IEstado
    {
        public void ExecutarAcao()
        {
            Console.WriteLine("Estado: Derrota");
            Console.WriteLine("Você perdeu o combate! O jogo acabou.");
        }
    }

    public static void Main(string[] args)
    {
        var controle = new Controle_de_Turno();
        controle.ExecutarTurno();  // Executa o primeiro turno (preparação)
        controle.ExecutarTurno();  // Executa o turno de combate
        controle.ExecutarTurno();  // Executa o turno de vitória
    }
}
