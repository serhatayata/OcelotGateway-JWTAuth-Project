using Writer.API.Models;

namespace Writer.API.Repositories.Abstract
{
    public interface IWriterRepository
    {
        List<WriterModel> GetAll();
        WriterModel? Get(int id);
        WriterModel Insert(WriterModel writer);
    }
}
