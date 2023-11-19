namespace Chapter._4._4.H.日誌框架.Json;

public record ExporterConfig(ExporterType Type, List<ExporterConfig> Children, string FileName);