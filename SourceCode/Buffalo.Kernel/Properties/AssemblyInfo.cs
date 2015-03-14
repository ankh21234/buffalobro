﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过下列属性集
// 控制。更改这些属性值可修改
// 与程序集关联的信息。
#if (NET_1_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 1.0")]
#elif (NET_1_1)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 1.1")]
#elif (NET_2_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 2.0")]
#elif (NET_3_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 3.0")]
#elif (NET_3_5)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 3.5")]
#elif (NET_4_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 4.0")]
#elif (NET_4_5)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 4.5")]
#elif (NET_4_5_1)
[assembly: AssemblyTitle("BuffaloKernel for .NET Framework 4.5.1")]
#elif (NETCF_1_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Compact Framework 1.0")]
#elif (NETCF_2_0)
[assembly: AssemblyTitle("BuffaloKernel for .NET Compact Framework 2.0")]
#elif (MONO_1_0)
[assembly: AssemblyTitle("BuffaloKernel for Mono 1.0")]
#elif (MONO_2_0)
[assembly: AssemblyTitle("BuffaloKernel for Mono 2.0")]
#else
[assembly: AssemblyTitle("BuffaloKernel")]
#endif

[assembly: AssemblyDescription("BuffaloKernelLibrary")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
[assembly: AssemblyProduct("Buffalo.Kernel(Debug)")]
#else
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyProduct("Buffalo.Kernel")]
#endif
[assembly: AssemblyCompany("Buffalo")]

[assembly: AssemblyCopyright("版权所有 (C) Buffalo 2012")]
[assembly: AssemblyTrademark("BuffaloLibrary")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 属性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("fb969b2a-2385-4e7e-83a5-e09d85a253c1")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“修订号”和“内部版本号”的默认值，
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion("1.0.3.7")]
[assembly: AssemblyFileVersion("1.0.3.7")]