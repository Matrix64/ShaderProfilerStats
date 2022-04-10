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
