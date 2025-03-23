using domain.Pojo.jobCore;

namespace jobcoreModule.Bll;

public interface IBlockLogBll
{

    void Save(BlockLog log);
    void Update(BlockLog log);

    BlockLog GetByTaskIdAndBId(string taskId, string bId);

}