using Chapter._4._2.H.好友關係分析器.AdvancedRequirements.Graph;
using Chapter._4._2.H.好友關係分析器.BasicRequirements.SuperRelationship;
using Chapter._4._2.H.好友關係分析器.Client;

var relationshipAnalyzerCli = new RelationshipAnalyzerCLI(new SuperRelationshipAnalyzerAdapter(), new RelationshipGraphAdapter());
relationshipAnalyzerCli.StartBasicRequirements();
relationshipAnalyzerCli.StartAdvancedRequirements();