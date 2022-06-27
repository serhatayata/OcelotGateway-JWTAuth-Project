using Article.API.Models;
using Article.API.Repositories.Abstract;

namespace Article.API.Repositories.Concrete
{
    public class ArticleRepository:IArticleRepository
    {
        private readonly static List<ArticleModel> _articles = Populate();

        private static List<ArticleModel> Populate()
        {
            var result = new List<ArticleModel>()
            {
                new ArticleModel
                {
                    Id = 1,
                    Title = "First Article",
                    WriterId = 1,
                    LastUpdate = DateTime.Now
                },
                new ArticleModel
                {
                    Id = 2,
                    Title = "Second title",
                    WriterId = 2,
                    LastUpdate = DateTime.Now
                },
                new ArticleModel
                {
                    Id = 3,
                    Title = "Third title",
                    WriterId = 3,
                    LastUpdate = DateTime.Now
                }
            };

            return result;
        }

        public List<ArticleModel> GetAll()
        {
            return _articles;
        }

        public ArticleModel? Get(int id)
        {
            return _articles.FirstOrDefault(x => x.Id == id);
        }

        public int Delete(int id)
        {
            var removed = _articles.SingleOrDefault(x => x.Id == id);

            if (removed != null)
                _articles.Remove(removed);

            return removed?.Id ?? 0;
        }
    }
}
