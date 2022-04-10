# ShaderProfilerStats
A tool for the shader profiler json file comparison

读取ShaderProfiler的JSON文件导出为CSV

## 输出模式

### 单表输入输出(Information Mode)

| Shader Name | Vertex Count | Fragment Count | Geometry Count | Hull Count | Domain Count |
|:-----------:|:------------:|:--------------:|:--------------:|:----------:|:------------:|
|   string    |     int      |      int       |      int       |    int     |     int      |
|     ...     |     ...      |      ...       |      ...       |    ...     |     ...      | 

### 双表比较输出(Compare Mode)

| Shader Name | Vertex Count | D-Value | Fragment Count | D-Value | Geometry Count | D-Value | Hull Count | D-Value | Domain Count | D-Value |
|:-----------:|:------------:|:-------:|:--------------:|:-------:|:--------------:|:-------:|:----------:|:-------:|:------------:|:-------:|
|   string    |     int      |   int   |      int       |   int   |      int       |   int   |    int     |   int   |     int      |   int   |
|     ...     |     ...      |   ...   |      ...       |   ...   |      ...       |   ...   |    ...     |   ...   |     ...      |   ...   |

## Blog Link

[为了提高ShaderProfiler可读性,我写了这个统计脚本 – Matrix 64 Blog](https://blog.matrix64.xyz/2022/04/10/%e4%b8%ba%e4%ba%86%e6%8f%90%e9%ab%98shaderprofiler%e5%8f%af%e8%af%bb%e6%80%a7%e6%88%91%e5%86%99%e4%ba%86%e8%bf%99%e4%b8%aa%e7%bb%9f%e8%ae%a1%e8%84%9a%e6%9c%ac/)
