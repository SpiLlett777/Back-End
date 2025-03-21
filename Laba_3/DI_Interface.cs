﻿namespace DI_Interface
{
    public interface ITool
    {
        public void Use();
    }
    public class Hammer : ITool
    {
        public void Use() => Console.WriteLine("Используем молоток: забиваем гвозди!");
    }
    public class Wrench : ITool
    {
        public void Use() => Console.WriteLine("Используем гаечный ключ: закручиваем гайки!");
    }
    public class Saw : ITool
    {
        public void Use() => Console.WriteLine("Используем пилу: пилим доску!");
    }

    public class Builder
    {
        private readonly ITool _hammer;
        private readonly ITool _wrench;
        private readonly ITool _saw;

        public Builder(ITool hammer, ITool wrench, ITool saw)
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
