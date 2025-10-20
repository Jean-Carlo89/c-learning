using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Verificador de Número de Fibonacci ---");


        int inputNumber = check_valid_integer_input("Digite um número inteiro positivo: ");


        int position = find_fibonacci_position(inputNumber);
        //***** Colocar a posicao maxima de forma dinamica posteriormente

        if (position >= 0)
        {
            Console.WriteLine($"Posição de índice {position}");
        }
        else
        {
            Console.WriteLine($"\nO número {inputNumber} não existe nas primeiras 1000 posićoes da sequência de Fibonacci.");
        }

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }


    public static int check_valid_integer_input(string prompt)
    {
        int number;
        string input;
        bool isValid = false;

        do
        {
            Console.Write(prompt);

            //** o Readline le a proxima linha que o usuario digitar
            //!!! Procurar depois a diferenca para os outros .REad
            input = Console.ReadLine();


            if (int.TryParse(input, out number))
            {

                if (number >= 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Digite um número inteiro positivo.");
                }
            }
            else
            {

                Console.WriteLine("Entrada inválida. Por favor, digite um número.");
            }

        } while (!isValid);

        return number;
    }

    public static int find_fibonacci_position(int user_input_number)
    {

        if (user_input_number == 0) return 0;
        if (user_input_number == 1) return 1;


        long first_aux = 0;
        long second_aux = 1;
        int position = 1;

        const int max_position = 1000;


        while (second_aux < user_input_number && position < max_position)
        {
            long temporary_aux = second_aux;
            second_aux = first_aux + second_aux;
            first_aux = temporary_aux;
            position++;


            if (second_aux == user_input_number)
            {
                return position;
            }
        }


        return -1;
    }
}