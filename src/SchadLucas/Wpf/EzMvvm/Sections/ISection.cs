namespace SchadLucas.Wpf.EzMvvm.Sections
{
    public interface ISection
    {
        object SectionContent { get; set; }
        object SectionContext { get; set; }
        string SectionName { get; set; }
        bool Visible { get; set; }
    }
}