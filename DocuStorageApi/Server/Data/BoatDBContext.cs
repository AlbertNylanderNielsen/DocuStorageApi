using DocuStorageApi.Shared;

namespace DocuStorageApi.Server.Data;

public class BoatDBContext
{
    public BoatDBContext()
    { 
        this.boat = new Boat
        {
            Id = 1
                 ,
            Name = "Test"
                 ,
            AllFiles = new List<FileReference>()
        };
    }

     public Boat boat { get; set; }
}
