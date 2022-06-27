using Writer.API.Models;
using Writer.API.Repositories.Abstract;

namespace Writer.API.Repositories.Concrete
{
    public class WriterRepository:List<WriterModel>,IWriterRepository
    {
        private readonly static List<WriterModel> _writers = Populate();

        private static List<WriterModel> Populate()
        {
            return new List<WriterModel>
            {
                new WriterModel
                {
                    Id = 1,
                    Name = "Leanne Graham"
                },
                new WriterModel
                {
                    Id = 2,
                    Name = "Ervin Howell"
                },
                new WriterModel
                {
                    Id = 3,
                    Name = "Glenna Reichert"
                }
            };
        }

        public List<WriterModel> GetAll()
        {
            return _writers;
        }

        public WriterModel Insert(WriterModel writer)
        {
            var maxId = _writers.Max(x => x.Id);

            writer.Id = ++maxId;
            _writers.Add(writer);

            return writer;
        }

        public WriterModel? Get(int id)
        {
            return _writers.FirstOrDefault(x => x.Id == id);
        }
    }
}
