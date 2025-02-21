namespace DI_Constructor
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
        private readonly Hammer _hammer;
        private readonly Wrench _wrench;
        private readonly Saw _saw;

        public Builder(Hammer hammer, Wrench wrench, Saw saw)
        {
            _hammer = hammer;
            _wrench = wrench;
            _saw = saw;
        }

        public void Build()
        {
            Console.WriteLine("Строитель начинает работу...");
            _hammer.Use();
            _wrench.Use();
            _saw.Use();
            Console.WriteLine("Строительство завершено!");
        }
    }
}
