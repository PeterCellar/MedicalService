using pbc = global::Google.Protobuf.Collections;
namespace MedicalService.Filtering;

public interface IFilter
{
    pbc::RepeatedField<DoublePair> DoubleFilter { get; }
    pbc::RepeatedField<StringPair> StringFilter { get; }
}