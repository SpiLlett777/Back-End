namespace Without_IoC
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

        public Builder()
        {
            _hammer = new Hammer();
            _wrench = new Wrench();
            _saw = new Saw();
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
