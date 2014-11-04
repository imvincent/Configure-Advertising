using System.IO;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;

namespace InstanceFactoy.ConfigureAdControlSample.Xml.Serialization
{
  /// <summary>
  /// Wrapper of <see cref="XmlSerializer"/> to ease the use of deserialization of objects.
  /// </summary>
  public static class XmlDeserializer
  {
    #region Public Static Methods

    /// <summary>
    /// Deserializes an object from an <see cref="INode"/>.
    /// </summary>
    /// <typeparam name="T">Typ of the object, that should be deserialized.</typeparam>
    /// <param name="serializedObject">The object to be deserialized.</param>
    /// <returns>The deserialisized object.</returns>
    public static T Deserialize<T>
      (
        IXmlNode serializedObject
      )
      where T : class
    {
      return (new XmlSerializer(typeof(T)).Deserialize(new StringReader(serializedObject.GetXml())) as T);
    }

    #endregion Public Static Methods
  }
}