using System;

namespace M1JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool simAI;
            string jogando = "S";
            string[,] espacos;

            while (jogando == "S")
            {
                string modoDeJogo;
                espacos = new string[3, 3]
                {
                    {" ", " ", " "},
                    {" ", " ", " "},
                    {" ", " ", " "}
                };

                Console.WriteLine("\nDigite <S> para Singleplayer (vs. AI) ou <M> para Multiplayer Local.");

                do
                {
                    modoDeJogo = Console.ReadLine().ToUpper();
                } while (modoDeJogo != "S" && modoDeJogo != "M");

                if (modoDeJogo == "S")
                {
                    simAI = true;
                }

                else
                {
                    simAI = false;
                }

                jogando = Jogar(espacos, simAI);
            }

        }

        static string Jogar(string[,] espacoDeJogo, bool aiEnabled)
        {
            string jogadorAtual = "X";
            int turnos = 0;
            string vencedor = "N/A";
            bool aiTurn = true;

            PrintarJogo(espacoDeJogo);

            while (vencedor == "N/A")
            {
                bool jogadorMudou = false;
                

                if(aiEnabled == true && aiTurn == true)
                {
                    espacoDeJogo = FazerJogadaAI(espacoDeJogo, jogadorAtual);
                }

                else
                {
                    espacoDeJogo = FazerJogada(espacoDeJogo, jogadorAtual);
                }
                
                PrintarJogo(espacoDeJogo);

                if (jogadorAtual == "X" && jogadorMudou == false)
                {
                    jogadorAtual = "O";
                    jogadorMudou = true;

                    aiTurn = false;
                }

                if (jogadorAtual == "O" && jogadorMudou == false)
                {
                    jogadorAtual = "X";
                    aiTurn = true;

                }

                vencedor = ChecarVitoria(espacoDeJogo);

                if (turnos == 8 && vencedor == "N/A")
                {
                    vencedor = "Empate!";
                }
                turnos++;
            }

            Console.WriteLine("\n E o resultado é: ");

            if (vencedor != "Empate!")
            {
                Console.WriteLine(vencedor + " venceu!");
            }

            else
            {
                Console.WriteLine(vencedor);
            }

            Console.WriteLine("\n\nGostaria de Jogar Novamente? Digite <S> se SIM, <N> se NÃO.");
            string respostaRejogar;

            do
            {
                respostaRejogar = Console.ReadLine().ToUpper();
            } while (respostaRejogar != "S" && respostaRejogar != "N");

            return respostaRejogar;
        }
        static string[,] FazerJogada(string[,] campo, string jogador)
        {
            int linhaJogada;
            int colunaJogada;


            do
            {
                linhaJogada = 0;
                colunaJogada = 0;
                Console.WriteLine("\nInsira o número da linha e dê ENTER, então insira o número da coluna do espaço de sua jogada.");

                while ((linhaJogada < 1 || linhaJogada > 3) || (colunaJogada < 1 || colunaJogada > 3))
                {
                    Console.Write("Linha: ");
                    linhaJogada = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Coluna: ");
                    colunaJogada = Convert.ToInt32(Console.ReadLine());
                }
            } while (campo[linhaJogada - 1, colunaJogada - 1] == "X" || campo[linhaJogada - 1, colunaJogada - 1] == "O");



            campo[linhaJogada - 1, colunaJogada - 1] = jogador;
            return campo;
        }

        static string[,] FazerJogadaAI(string[,] campoAI, string jogadorAI)
        {
            int linhaGerada;
            int colunaGerada;
            var rngGerador = new Random();
            int valorAleatorio;

            for(int iteradorLinha = 0; iteradorLinha < 3; iteradorLinha++)
            {
                for(int iteradorColuna = 0; iteradorColuna < 3; iteradorColuna++)
                {
                    if (campoAI[iteradorLinha, iteradorColuna] == " ")
                    {
                        campoAI[iteradorLinha, iteradorColuna] = jogadorAI;

                        if (ChecarVitoria(campoAI) == "Jogador 1(X)")
                        {
                            return campoAI;
                        }
                        
                        campoAI[iteradorLinha, iteradorColuna] = " ";
                        
                    }
                }
            }


            for (int iteradorLinhaDerrota = 0; iteradorLinhaDerrota < 3; iteradorLinhaDerrota++)
            {
                for (int iteradorColunaDerrota = 0; iteradorColunaDerrota < 3; iteradorColunaDerrota++)
                {
                    if (campoAI[iteradorLinhaDerrota, iteradorColunaDerrota] == " ")
                    {
                        jogadorAI = "O";
                        campoAI[iteradorLinhaDerrota, iteradorColunaDerrota] = jogadorAI;
                        jogadorAI = "X";

                        if (ChecarVitoria(campoAI) == "Jogador 2(O)")
                        {
                            campoAI[iteradorLinhaDerrota, iteradorColunaDerrota] = jogadorAI;
                            return campoAI;
                        }

                        campoAI[iteradorLinhaDerrota, iteradorColunaDerrota] = " ";
                    }
                }
            }


            do
            {
                valorAleatorio = rngGerador.Next(0, 3);
                linhaGerada = valorAleatorio;
                valorAleatorio = rngGerador.Next(0, 3);
                colunaGerada = valorAleatorio;
            } while (campoAI[linhaGerada, colunaGerada] == "X" || campoAI[linhaGerada, colunaGerada] == "O");

            campoAI[linhaGerada, colunaGerada] = jogadorAI;
            return campoAI;
            
        }

        static string ChecarVitoria(string[,] tabuleiroAtual)
        {
            for(int i = 0; i < tabuleiroAtual.GetLength(0); i++)
            {
                if (tabuleiroAtual[i,0] == tabuleiroAtual[i,1] && tabuleiroAtual[i,0] == tabuleiroAtual[i, 2] && tabuleiroAtual[i,0] != " ")
                {
                    if (tabuleiroAtual[i,0] == "X")
                    {
                        return "Jogador 1(X)";
                    } 

                    else
                    {
                        return "Jogador 2(O)";
                    }
                }

                else if (tabuleiroAtual[0,i] == tabuleiroAtual[1,i] && tabuleiroAtual[0,i] == tabuleiroAtual[2,i] && tabuleiroAtual[0,i] != " ")
                {
                    if (tabuleiroAtual[0, i] == "X")
                    {
                        return "Jogador 1(X)";
                    }

                    else
                    {
                        return "Jogador 2(O)";
                    }
                }
            }

            if (tabuleiroAtual[0,0] == tabuleiroAtual[1,1] && tabuleiroAtual[0,0] == tabuleiroAtual[2,2] && tabuleiroAtual[0,0] != " ")
            {
                if (tabuleiroAtual[0, 0] == "X")
                {
                    return "Jogador 1(X)";
                }

                else
                {
                    return "Jogador 2(O)";
                }
            }

            if (tabuleiroAtual[2,0] == tabuleiroAtual[1,1] && tabuleiroAtual[0,2] == tabuleiroAtual[2,0] && tabuleiroAtual[2,0] != " ")
            {
                if (tabuleiroAtual[2,0] == "X")
                {
                    return "Jogador 1(X)";
                }

                else
                {
                    return "Jogador 2(O)";
                }
            }
            return "N/A";

        }
        static void PrintarJogo(string[,] campoAtual)
        {
            Console.Clear();
            Console.WriteLine("Olá! Esse é o simulador de Jogo da Velha");
            Console.WriteLine("Feito por Lorenzo Grando e Pedro Henrique D'Avila");
            Console.WriteLine();
            Console.WriteLine(
               "          COLUNA" +
               "\n        1   2   3" +
               "\n  L  1  " + campoAtual[0,0] + " | " + campoAtual[0,1] + " | " + campoAtual[0,2] + " " +
               "\n  I     ----------" +
               "\n  N  2  " + campoAtual[1,0] + " | " + campoAtual[1,1] + " | " + campoAtual[1,2] + " " +
               "\n  H     ----------" +
               "\n  A  3  " + campoAtual[2,0] + " | " + campoAtual[2,1] + " | " + campoAtual[2,2] + " "
            );
        }
    }
}