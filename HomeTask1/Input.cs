namespace HomeTask1
{
    internal class Input
    {
        public string Path { get; }

        public Input(string path)
        {
            Path = path;

            watcher.Changed += async (sender, e) => { }
        }
    }
}
