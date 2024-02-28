using System.ComponentModel.Design;

namespace PrivateMultipleObjects
{
    class Drink
    {
        protected string _name;
        protected string _size;
        protected int _quantity;

        public Drink()
        {
            _name = string.Empty;
            _size = string.Empty;
            _quantity = 0;
        }
        public Drink(string name, string size, int quantity)
        {
            _name = name;
            _size = size;
            _quantity = quantity;
        }

        public string GetName() { return _name; }
        public string GetSize() { return _size; }
        public int GetQuantity() { return _quantity; }
        public void SetName(string name) { _name = name; }
        public void SetSize(string size) { _size = size; }
        public void SetQuantity(int quantity) { _quantity = quantity; }

        public virtual void AddChange()
        {
            Console.WriteLine("What would you like to drink?");
            _name = Console.ReadLine();
            Console.WriteLine("What size?");
            _size = Console.ReadLine();
            Console.WriteLine("How many?");
            _quantity = int.Parse(Console.ReadLine());
        }
        public virtual void Display()
        {
            Console.WriteLine("\n*--------------------*");
            Console.WriteLine("Here's your order:");
            Console.WriteLine($"{_name}");
            Console.WriteLine($"Size: {_size}");
            Console.WriteLine($"Quantity: {_quantity}");
        }
    }

    class Coffee : Drink
    {
        protected string _syrup;
        protected string _toppings;

        public Coffee()
            : base()
        {
            _syrup = string.Empty;
            _toppings = string.Empty;
        }
        public Coffee(string name, string size, int quantity, string syrup, string toppings)
            : base(name, size, quantity)
        {
            _syrup = syrup;
            _toppings = toppings;
        }

        public string GetSyrup() { return _syrup; }
        public string GetToppings() { return _toppings; }
        public void SetSyrup(string syrup) { _syrup = syrup; }
        public void SetToppings(string toppings) { _toppings = toppings; }

        public override void AddChange()
        {
            base.AddChange();
            Console.WriteLine("Which syrup do you want?");
            _syrup = Console.ReadLine();
            Console.WriteLine("Which toppings do you want?");
            _toppings = Console.ReadLine();
        }
        public override void Display()
        {
            base.Display();
            Console.WriteLine($"Syrup: {_syrup}");
            Console.WriteLine($"Toppings: {_toppings}");
            Console.WriteLine("Thank you!");
            Console.WriteLine("*--------------------*\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many drink would you like to order?");
            int order;
            while (!int.TryParse(Console.ReadLine(), out order))
                Console.WriteLine("Please enter a whole number!");
            Drink[] drink = new Drink[order];
            Console.WriteLine("How many coffee would you like to order?");
            int cOrder;
            while (!int.TryParse(Console.ReadLine(), out cOrder))
                Console.WriteLine("Please enter a whole number!");
            Coffee[] coffee = new Coffee[cOrder];

            int choice, num, type;
            int drinkCounter = 0, coffeeCounter = 0;
            choice = Menu();
            while (choice != 4)
            {
                Console.WriteLine("Enter 1 for Coffee or 2 for Drink");
                while (!int.TryParse(Console.ReadLine(), out type))
                    Console.WriteLine("1 for Coffee or 2 for Drink");
                try
                {
                    switch (choice)
                    {
                        case 1:
                            if (type == 1)
                            {
                                if (coffeeCounter <= cOrder)
                                {
                                    coffee[coffeeCounter] = new Coffee();
                                    coffee[coffeeCounter].AddChange();
                                    coffeeCounter++;
                                }
                                else
                                    Console.WriteLine("All of your coffee order have been added");
                            }
                            else
                            {
                                if (drinkCounter <= order)
                                {
                                    drink[drinkCounter] = new Drink();
                                    drink[drinkCounter].AddChange();
                                    drinkCounter++;
                                }
                                else
                                    Console.WriteLine("All of your drink order have been added");
                            }
                            break;
                        case 2:
                            Console.Write("Enter the order number you want to change: ");
                            while (!int.TryParse(Console.ReadLine(), out num))
                                Console.Write("Enter the order number you want to change: ");
                            num--;
                            if (type == 1)
                            {
                                while (num > coffeeCounter - 1 || num < 0)
                                {
                                    Console.WriteLine("The number you entered was out of range, try again!");
                                    while (!int.TryParse(Console.ReadLine(), out num))
                                        Console.Write("Enter the order number you want to change: ");
                                    num--;
                                }
                                coffee[num].AddChange();
                            }
                            else
                            {
                                while (num > drinkCounter - 1 || num < 0)
                                {
                                    Console.WriteLine("The number you entered was out of range, try again!");
                                    while (!int.TryParse(Console.ReadLine(), out num))
                                        Console.Write("Enter the order number you want to change: ");
                                    num--;
                                }
                                drink[num].AddChange();
                            }
                            break;
                        case 3:
                            if (type == 1)
                            {
                                for (int i = 0; i < coffeeCounter; i++)
                                    coffee[i].Display();
                            }
                            else
                            {
                                for (int i = 0; i < drinkCounter; i++)
                                    drink[i].Display();
                            }
                            break;
                        default:
                            Console.WriteLine("You made an invalid selection, please try again");
                            break;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                choice = Menu();
            }
        }

        private static int Menu()
        {
            Console.WriteLine("Please make a selection from the menu");
            Console.WriteLine("1-Add 2-Change 3-Print 4-Quit");
            int selection = 0;
            while (selection < 1 || selection > 4)
                while (!int.TryParse(Console.ReadLine(), out selection))
                    Console.WriteLine("1-Add 2-Change 3-Print 4-Quit");
            return selection;
        }
    }
}
