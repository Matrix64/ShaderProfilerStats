namespace ShaderProfiler.Editor
{
    public class SerializedShaderData
    {
        public ShaderInfoList[] shaderInfoList { get; set; }

        public class ShaderInfoList
        {
            public int MemorySize { get; set; }
            public uint ActiveSubshaderIndex { get; set; }
            public string ShaderName { get; set; }
            public SubshaderInfos[] SubshaderInfos { get; set; }
        }

        public class SubshaderInfos
        {
            public int SubshaderLOD { get; set; }
            public PassInfos[] PassInfos { get; set; }
        }

        public class PassInfos
        {
            public bool IsValid { get; set; }
            public string PassName { get; set; }
            public ProgramInfos ProgramInfos { get; set; }
        }

        public class ProgramInfos
        {
            public Vertex Vertex { get; set; }
            public Fragment Fragment { get; set; }
            public Geometry Geometry { get; set; }
            public Hull Hull { get; set; }
            public Domain Domain { get; set; }
        }

        public class Vertex
        {
            public VariatInfos[] VariatInfos { get; set; }
        }

        public class Fragment
        {
            public VariatInfos[] VariatInfos { get; set; }
        }

        public class Geometry
        {
            public VariatInfos[] VariatInfos { get; set; }
        }

        public class Hull
        {
            public VariatInfos[] VariatInfos { get; set; }
        }

        public class Domain
        {
            public VariatInfos[] VariatInfos { get; set; }
        }

        public class VariatInfos
        {
            public bool IsWarmup { get; set; }
            public string Keywords { get; set; }
        }
    }
}