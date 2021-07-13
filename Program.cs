﻿using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
        }

        private static void ExcluirSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			Console.WriteLine("Para excluir a série, digite (EXCLUIR)");
			string confirma= Console.ReadLine();
			Console.WriteLine();

			if(confirma == "EXCLUIR")
			{
				repositorio.Exclui(indiceSerie);
				Console.WriteLine("Série excluída.");
			}
			else
			{
				Console.WriteLine("A série não foi excluida.");
			}
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("Digite o ID da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			ListarGenero();
            
			int entradaGenero, entradaAno;
            string entradaTitulo, entradaDescricao;
            InfoSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}

         private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            ListarGenero();
            
            int entradaGenero, entradaAno;
            string entradaTitulo, entradaDescricao;
            InfoSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void InfoSerie(out int entradaGenero, out string entradaTitulo, out int entradaAno, out string entradaDescricao)
        {
            Console.Write("Digite o gênero entre as opções acima: ");
            entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Digite o Título da Série: ");
            entradaTitulo = Console.ReadLine();
            Console.Write("Digite o Ano de Início da Série: ");
            entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Digite a Descrição da Série: ");
            entradaDescricao = Console.ReadLine();
        }

        private static void ListarGenero()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
			Console.WriteLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;

            }
                foreach ( var serie in lista)
                {
                    var excluido = serie.retornaExcluido();
            
                    Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), excluido ? "*Excluído*" : "");
                }
        }



        

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor !");
			Console.WriteLine();
			Console.WriteLine("Informe a opção desejada: ");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
