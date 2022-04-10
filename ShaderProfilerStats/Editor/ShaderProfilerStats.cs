using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ShaderProfiler.Editor;
using UnityEditor;
using UnityEngine;

public class ShaderProfilerStats
{
    public enum ProgramType
    {
        Vertex,
        Fragment,
        Geometry,
        Hull,
        Domain
    }

    private static ProfilerStatsItem _currentStatsItem;
    private static ProfilerStatsItem _compareStatsItem;
    private Vector2 _scrollViewPos;


    [MenuItem("Tools/Shader Profiler stats/Information Mode", false, 200)]
    public static void SingleJSONToCSV()
    {
        LoadJSONFile();
        SaveToCSV();
    }

    [MenuItem("Tools/Shader Profiler stats/Compare Mode", false, 200)]
    public static void DoubleJSONToCSV()
    {
        LoadJSONFile(true);
        SaveToCSV();
    }

    private static void LoadJSONFile(bool compareMode = false)
    {
        string path = EditorUtility.OpenFilePanel("Load target JSON", Application.dataPath, "json");
        if (path != "")
        {
            ProfilerStatsItem newStats = new ProfilerStatsItem();
            newStats.ShaderData = JsonConvert.DeserializeObject<SerializedShaderData>(File.ReadAllText(path));
            newStats.ItemName = Path.GetFileNameWithoutExtension(path);

            _currentStatsItem = newStats;
        }

        if (compareMode)
        {
            string path2 = EditorUtility.OpenFilePanel("Load Compare JSON", Application.dataPath, "json");
            if (path != "")
            {
                ProfilerStatsItem newStats = new ProfilerStatsItem();
                newStats.ShaderData = JsonConvert.DeserializeObject<SerializedShaderData>(File.ReadAllText(path2));
                newStats.ItemName = Path.GetFileNameWithoutExtension(path);

                _compareStatsItem = newStats;
            }
        }
    }

    private static string GetPath(string dataName)
    {
        var path = Application.dataPath + "/CSV/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path += dataName + ".csv";
        return path;
    }

    private static Dictionary<ProgramType, Dictionary<string, int>> GetVariantsCount(ProfilerStatsItem statsItem)
    {
        Dictionary<ProgramType, Dictionary<string, int>> variantsDict =
            new Dictionary<ProgramType, Dictionary<string, int>>();
        Dictionary<string, int> vertexDict = new Dictionary<string, int>();
        Dictionary<string, int> fragmentDict = new Dictionary<string, int>();
        Dictionary<string, int> geometryDict = new Dictionary<string, int>();
        Dictionary<string, int> hullDict = new Dictionary<string, int>();
        Dictionary<string, int> domainDict = new Dictionary<string, int>();
        var combinedList = statsItem.ShaderData.shaderInfoList
            .GroupBy(a => a.ShaderName)
            .Select(b => new
            {
                ShaderName = b.Key,
                VertexVariatInfos = b.Select(c => c.SubshaderInfos)
                    .Select(d => d
                        .Select(g => g.PassInfos)
                        .Select(f => f
                            .Select(g => g.ProgramInfos)
                            .Select(h => h.Vertex).Where(item => item != null)
                            .Select(i => i.VariatInfos))
                    ),
                FragmentVariatInfos = b.Select(c => c.SubshaderInfos)
                    .Select(d => d
                        .Select(g => g.PassInfos)
                        .Select(f => f
                            .Select(g => g.ProgramInfos)
                            .Select(h => h.Fragment).Where(item => item != null)
                            .Select(i => i.VariatInfos))
                    ),
                GeometryVariatInfos = b.Select(c => c.SubshaderInfos)
                    .Select(d => d
                        .Select(g => g.PassInfos)
                        .Select(f => f
                            .Select(g => g.ProgramInfos)
                            .Select(h => h.Geometry).Where(item => item != null)
                            .Select(i => i.VariatInfos))
                    ),
                HullVariatInfos = b.Select(c => c.SubshaderInfos)
                    .Select(d => d
                        .Select(g => g.PassInfos)
                        .Select(f => f
                            .Select(g => g.ProgramInfos)
                            .Select(h => h.Hull).Where(item => item != null)
                            .Select(i => i.VariatInfos))
                    ),
                DomainVariatInfos = b.Select(c => c.SubshaderInfos)
                    .Select(d => d
                        .Select(g => g.PassInfos)
                        .Select(f => f
                            .Select(g => g.ProgramInfos)
                            .Select(h => h.Domain).Where(item => item != null)
                            .Select(i => i.VariatInfos))
                    ),
            }).ToList();

        
        foreach (var bassValueDict in combinedList)
        {
            vertexDict.Add(bassValueDict.ShaderName, 0);
            fragmentDict.Add(bassValueDict.ShaderName, 0);
            geometryDict.Add(bassValueDict.ShaderName, 0);
            hullDict.Add(bassValueDict.ShaderName, 0);
            domainDict.Add(bassValueDict.ShaderName, 0);

            foreach (var IEIEInfos in bassValueDict.VertexVariatInfos)
            {
                foreach (var IEInfos in IEIEInfos)
                {
                    foreach (var Infos in IEInfos)
                    {
                        vertexDict[bassValueDict.ShaderName] += Infos.Count();
                    }
                }
            }

            foreach (var IEIEInfos in bassValueDict.FragmentVariatInfos)
            {
                foreach (var IEInfos in IEIEInfos)
                {
                    foreach (var Infos in IEInfos)
                    {
                        fragmentDict[bassValueDict.ShaderName] += Infos.Count();
                    }
                }
            }

            foreach (var IEIEInfos in bassValueDict.GeometryVariatInfos)
            {
                foreach (var IEInfos in IEIEInfos)
                {
                    foreach (var Infos in IEInfos)
                    {
                        geometryDict[bassValueDict.ShaderName] += Infos.Count();
                    }
                }
            }

            foreach (var IEIEInfos in bassValueDict.HullVariatInfos)
            {
                foreach (var IEInfos in IEIEInfos)
                {
                    foreach (var Infos in IEInfos)
                    {
                        hullDict[bassValueDict.ShaderName] += Infos.Count();
                    }
                }
            }

            foreach (var IEIEInfos in bassValueDict.DomainVariatInfos)
            {
                foreach (var IEInfos in IEIEInfos)
                {
                    foreach (var Infos in IEInfos)
                    {
                        domainDict[bassValueDict.ShaderName] += Infos.Count();
                    }
                }
            }
        }


        variantsDict.Add(ProgramType.Vertex, vertexDict);
        variantsDict.Add(ProgramType.Fragment, fragmentDict);
        variantsDict.Add(ProgramType.Geometry, geometryDict);
        variantsDict.Add(ProgramType.Hull, hullDict);
        variantsDict.Add(ProgramType.Domain, domainDict);

        return variantsDict;
    }

    private static void SaveToCSV()
    {
        if (_currentStatsItem == null) return;

        string filePath = GetPath(_currentStatsItem.ItemName);
        StreamWriter writer = new StreamWriter(filePath);
        var currentVariantsCount = GetVariantsCount(_currentStatsItem);
        if (_compareStatsItem == null)
        {
            writer.WriteLine("ShaderName,VertexCount,FragmentCount,GeometryCount,HullCount,DomainCount");

            for (int i = 0; i < currentVariantsCount[ProgramType.Vertex].Count; i++)
            {
                var shaderName = currentVariantsCount[ProgramType.Vertex].ElementAt(i).Key;

                int vertexCount = currentVariantsCount[ProgramType.Vertex][shaderName];
                int fragmentCount = currentVariantsCount[ProgramType.Fragment][shaderName];
                int geometryCount = currentVariantsCount[ProgramType.Geometry][shaderName];
                int hullCount = currentVariantsCount[ProgramType.Hull][shaderName];
                int domainCount = currentVariantsCount[ProgramType.Domain][shaderName];

                var q = i + 1;
                Debug.Log(q + " " + shaderName);
                writer.WriteLine( shaderName + "," +
                                  vertexCount + "," +
                                  fragmentCount + "," +
                                  geometryCount + "," +
                                  hullCount + "," +
                                  domainCount);
            }
        }
        else
        {
            writer.WriteLine(
                "ShaderName,VertexCount,D-value,FragmentCount,D-value,GeometryCount,D-value,HullCount,D-value,DomainCount,D-value");
            var compareVariantsCount = GetVariantsCount(_compareStatsItem);
            for (int i = 0; i < currentVariantsCount[ProgramType.Vertex].Count; i++)
            {
                var shaderName = currentVariantsCount[ProgramType.Vertex].ElementAt(i).Key;

                int vertexCount = currentVariantsCount[ProgramType.Vertex][shaderName];
                int fragmentCount = currentVariantsCount[ProgramType.Fragment][shaderName];
                int geometryCount = currentVariantsCount[ProgramType.Geometry][shaderName];
                int hullCount = currentVariantsCount[ProgramType.Hull][shaderName];
                int domainCount = currentVariantsCount[ProgramType.Domain][shaderName];

                int vertexCountD = vertexCount;
                int fragmentCountD = fragmentCount;
                int geometryCountD = geometryCount;
                int hullCountD = hullCount;
                int domainCountD = domainCount;

                foreach (var VariantsDict in compareVariantsCount[ProgramType.Vertex])
                {
                    if (VariantsDict.Key.Equals(shaderName))
                    {
                        vertexCountD = vertexCount - compareVariantsCount[ProgramType.Vertex][shaderName];
                        fragmentCountD = fragmentCount - compareVariantsCount[ProgramType.Fragment][shaderName];
                        geometryCountD = geometryCount - compareVariantsCount[ProgramType.Geometry][shaderName];
                        hullCountD = hullCount - compareVariantsCount[ProgramType.Hull][shaderName];
                        domainCountD = domainCount - compareVariantsCount[ProgramType.Domain][shaderName];
                    }
                }

                writer.WriteLine(shaderName + "," +
                                 vertexCount + "," +
                                 vertexCountD + "," +
                                 fragmentCount + "," +
                                 fragmentCountD + "," +
                                 geometryCount + "," +
                                 geometryCountD + "," +
                                 hullCount + "," +
                                 hullCountD + "," +
                                 domainCount + "," +
                                 domainCountD);
            }
        }

        writer.Flush();
        writer.Close();

        _currentStatsItem = null;
        _compareStatsItem = null;

        Debug.Log("Export CSV Done");
    }

    public class ProfilerStatsItem
    {
        public SerializedShaderData ShaderData;
        public string ItemName;
    }
}
