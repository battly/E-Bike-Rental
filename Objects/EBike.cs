using System.Xml.Serialization;

public class EBike
{
    [XmlElement("Guid")]
    public Guid Guid { get; set; }

    [XmlElement("Producer")]
    public string Producer { get; set; }

    [XmlElement("Model")]
    public string Model { get; set; }

    [XmlElement("Performance")]
    public double Performance { get; set; }



    public EBike(string producer, string model, double performance)
    {
        Guid = Guid.NewGuid();
        Producer = producer;
        Model = model;
        Performance = performance;
    }

    public EBike(Guid guid, string producer, string model, double performance)
    {
        Guid = guid;
        Producer = producer;
        Model = model;
        Performance = performance;
    }


    public override string ToString()
    {
        return this.Producer + this.Model + this.Performance;
    }

}