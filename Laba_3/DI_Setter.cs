namespace DI_Setter
{
    public class Hammer
    {
        public void Use() => Console.WriteLine("Используем молоток: забиваем гвозди!");
    }
    public class Wrench
    {
        public void Use() => Console.WriteLine("Используем гаечный ключ: закручиваем гайки!");
    }
    public class Saw
    {
        public void Use() => Console.WriteLine("Используем пилу: пилим доску!");
    }

    public class Builder
    {
        public Hammer Hammer { get; set; }
        public Wrench Wrench { get; set; }
        public Saw Saw { get; set; }

        public void Build()
        {
            if (Hammer is null || Wrench is null || Saw is null)
            {
                throw new Exception("Не все инструменты имеются у строителя...");
            }

            Console.WriteLine("Строитель начинает работу...");
            Hammer.Use();
            Wrench.Use();
            Saw.Use();
            Console.WriteLine("Строительство завершено!");
        }
    }
}
