using Article.API.Models;

namespace Article.API.Repositories.Abstract
{
    public interface IArticleRepository
    {
        List<ArticleModel> GetAll();
        ArticleModel? Get(int id);
        int Delete(int id);
    }
}
