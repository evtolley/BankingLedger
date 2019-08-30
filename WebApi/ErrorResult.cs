namespace WebApi
{
    public class ErrorResult
    {
        public string Title { get; }

        public ErrorResult(string title)
        {
            Title = title;
        }
    }
}
