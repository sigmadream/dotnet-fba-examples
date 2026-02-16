#!/usr/bin/env -S dotnet fsi
// ============================================================================
// F# Shell Scripts — Standard Shebang
// ============================================================================
// 표준 shebang 방식을 사용한 기본 F# 스크립트입니다.
// __SOURCE_FILE__ 과 __SOURCE_DIRECTORY__ 를 사용하여 스크립트 경로를 출력합니다.
// ============================================================================
printfn "No lemon, no melon."
printfn "File: %s" __SOURCE_FILE__
printfn "Directory: %s" __SOURCE_DIRECTORY__
