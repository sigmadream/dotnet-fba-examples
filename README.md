# C# File-Based Application (FBA) Zero to Hero

> 🚀 .NET의 강력한 FBA(File-Based Application) 기능을 활용한 실전 예제 모음

C# File-Based Application (FBA) 기능을 활용한 다양한 예제 코드 모음입니다. .NET 10 이상의 FBA 기능을 사용하여 단일 파일로 작성된 실행 가능한 C# 스크립트 예제들을 제공합니다.

> 발표 자료 같이보기: <https://1drv.ms/p/c/318484c5aad6b73d/EQNehpg4jN5CmVcTh-qDoecBGqBe3gMwuGh3bbHf7EW4yQ?e=M1RstJ>

## 🎯 FBA란 무엇인가요?

File-Based Application(FBA)은 .NET의 혁신적인 기능으로, 단일 C# 파일을 프로젝트 파일(.csproj) 없이 직접 실행할 수 있게 해줍니다. 이는 다음과 같은 장점을 제공합니다:

- **✨ 간편한 시작**: 프로젝트 구조 없이 단일 파일로 애플리케이션 작성
- **🔧 빠른 프로토타이핑**: 아이디어를 빠르게 테스트하고 검증
- **📦 의존성 관리**: `#:package` 디렉티브로 NuGet 패키지 직접 참조
- **🎭 SDK 선택**: `#:sdk` 디렉티브로 필요한 SDK 지정
- **⚙️ 빌드 속성**: `#:property` 디렉티브로 MSBuild 속성 설정
- **🐚 스크립트 실행**: Shebang(`#!/usr/bin/env dotnet`)을 통한 스크립트 실행

## 📋 목차

- [C# File-Based Application (FBA) Zero to Hero](#c-file-based-application-fba-zero-to-hero)
  - [🎯 FBA란 무엇인가요?](#-fba란-무엇인가요)
  - [📋 목차](#-목차)
  - [💻 요구사항](#-요구사항)
    - [필수 요구사항](#필수-요구사항)
    - [선택적 요구사항](#선택적-요구사항)
    - [개발 도구 (권장)](#개발-도구-권장)
  - [🚀 빠른 시작](#-빠른-시작)
  - [📚 예제 설명](#-예제-설명)
    - [01. Shell Scripts](#01-shell-scripts)
      - [`01-shell-standard.cs`](#01-shell-standardcs)
      - [`01-shell-alt.cs`](#01-shell-altcs)
    - [02. Web API](#02-web-api)
      - [`02-random-webapi.cs`](#02-random-webapics)
    - [03. Aspire App Host](#03-aspire-app-host)
      - [구성 요소](#구성-요소)
    - [04. Avalonia GUI](#04-avalonia-gui)
      - [`04-avalonia.cs`](#04-avaloniacs)
    - [05. Native AOT](#05-native-aot)
      - [예제 파일](#예제-파일)
      - [동적 라이브러리 (.dylib) 예제](#동적-라이브러리-dylib-예제)
      - [정적 라이브러리 (.a) 예제](#정적-라이브러리-a-예제)
    - [06. AG-UI (AI Agents)](#06-ag-ui-ai-agents)
    - [07. Blazor WebAssembly](#07-blazor-webassembly)
      - [구성 요소](#구성-요소-1)
      - [프로젝트 구조](#프로젝트-구조)
  - [📖 FBA 디렉티브 가이드](#-fba-디렉티브-가이드)
    - [SDK 지정](#sdk-지정)
    - [패키지 참조](#패키지-참조)
    - [빌드 속성](#빌드-속성)
    - [Shebang 스타일](#shebang-스타일)
  - [🔧 문제 해결](#-문제-해결)
    - [실행 권한 오류](#실행-권한-오류)
    - [패키지 복원 오류](#패키지-복원-오류)
    - [Native AOT 빌드 실패](#native-aot-빌드-실패)
    - [Aspire Dashboard 접근 불가](#aspire-dashboard-접근-불가)
    - [OpenRouter API 키 오류](#openrouter-api-키-오류)
  - [🤝 기여하기](#-기여하기)
  - [📄 라이선스](#-라이선스)
  - [🔗 참고 자료](#-참고-자료)
    - [공식 문서](#공식-문서)
    - [프레임워크 및 도구](#프레임워크-및-도구)
    - [커뮤니티](#커뮤니티)
    - [관련 프로젝트](#관련-프로젝트)

## 💻 요구사항

### 필수 요구사항

- **[.NET 10.0 SDK](https://dotnet.microsoft.com/download)** 이상
  - FBA 기능을 완전히 지원하는 최신 버전 사용 권장
  - 설치 확인: `dotnet --version`
- **운영체제**: macOS, Linux, 또는 Windows
  - 모든 예제는 크로스 플랫폼으로 설계됨

### 선택적 요구사항

- **Python 3.x** (05-native-aot.py 실행 시)
  - Native AOT 라이브러리 호출 예제에 필요
- **Java 17+** 및 **wget** (05-native-aot.java 실행 시)
  - Java에서 Native AOT 라이브러리 호출 예제에 필요
  - JNA(Java Native Access) 라이브러리 자동 다운로드
- **Node.js 18+** 및 **npm** (05-native-aot.js 실행 시)
  - Node.js에서 Native AOT 라이브러리 호출 예제에 필요
  - koffi 패키지 자동 설치
- **GCC/Clang** (05-staticlib-c.c 컴파일 시)
  - C 컴파일러 및 iconv 라이브러리
  - macOS: Xcode Command Line Tools (`xcode-select --install`)
  - Linux: `build-essential` 패키지
- **OpenRouter API 키** (06-agui-server.cs 실행 시)
  - AI 에이전트 예제에 필요
  - [OpenRouter](https://openrouter.ai/)에서 무료 계정 생성 가능

### 개발 도구 (권장)

- **Visual Studio Code** + C# Dev Kit 확장
- **JetBrains Rider**
- **Visual Studio 2022** (Windows)
- **LINQPad 8** (이 저장소의 원래 실행 환경)

## 🚀 빠른 시작

```bash
# 저장소 클론
git clone <repository-url>
cd csharp-fba-zero-to-hero

# 가장 간단한 예제 실행
chmod +x 01-shell-standard.cs
./01-shell-standard.cs

# 웹 API 예제 실행
chmod +x 02-random-webapi.cs
./02-random-webapi.cs
# 다른 터미널에서: curl http://localhost:5000/
```

## 📚 예제 설명

각 예제는 FBA의 특정 기능과 실제 사용 사례를 보여줍니다. 난이도 순으로 정렬되어 있으며, 기초부터 고급 주제까지 다룹니다.

### 01. Shell Scripts

> 🎓 **학습 목표**: FBA의 기본 Shebang 사용법과 스크립트 실행 방식 이해

간단한 "Hello World" 스타일의 쉘 스크립트 예제입니다. C# 파일을 Unix/Linux 스타일의 실행 가능한 스크립트로 만드는 방법을 보여줍니다.

**핵심 개념:**

- Shebang(`#!`)을 사용한 인터프리터 지정
- 실행 권한 부여 (`chmod +x`)
- 직접 실행 가능한 C# 파일

#### `01-shell-standard.cs`

표준 shebang 방식을 사용한 기본 스크립트입니다.

**실행 방법:**

```bash
# 실행 권한 부여
chmod +x 01-shell-standard.cs

# 실행
./01-shell-standard.cs
```

**출력:**

```text
No lemon, no melon.
```

#### `01-shell-alt.cs`

대체 shebang 방식을 사용한 스크립트입니다. 이 방식은 JBang에서 사용하는 방식과 같은 방식입니다.

**실행 방법:**

```bash
# 실행 권한 부여
chmod +x 01-shell-alt.cs

# 실행
./01-shell-alt.cs
```

**출력:**

```text
No lemon, No melon.
```

---

### 02. Web API

> 🎓 **학습 목표**: FBA에서 ASP.NET Core 웹 애플리케이션 작성 방법, SDK 지정 및 의존성 주입 이해

ASP.NET Core를 사용한 간단한 웹 API 서버입니다. 랜덤 숫자를 반환하는 엔드포인트를 제공합니다.

**핵심 개념:**

- `#:sdk Microsoft.NET.Sdk.Web` - Web SDK 사용
- `#:property PublishAot=false` - AOT 컴파일 비활성화
- Minimal API 패턴
- Dependency Injection (DI)
- Singleton 서비스 등록 (`Random.Shared`)

#### `02-random-webapi.cs`

**실행 방법:**

```bash
# 실행 권한 부여
chmod +x 02-random-webapi.cs

# 실행
./02-random-webapi.cs
```

**테스트:**

```bash
# 다른 터미널에서 실행
curl http://localhost:5000/
```

**응답 예시:**

```json
{
  "ts": "2025-11-26T12:34:56.7890123Z",
  "val": 7
}
```

---

### 03. Aspire App Host

> 🎓 **학습 목표**: .NET Aspire를 사용한 분산 애플리케이션 구축, 서비스 간 통신 및 오케스트레이션

.NET Aspire를 사용한 분산 애플리케이션 오케스트레이션 예제입니다. Garnet(Redis 호환) 캐시 서버, Worker 서비스, Minimal API를 함께 실행합니다.

**핵심 개념:**

- `#:sdk Aspire.AppHost.Sdk` - Aspire AppHost SDK 사용
- `#:package` 디렉티브로 NuGet 패키지 참조
- 서비스 디스커버리 및 참조 관리
- `WaitFor` 패턴으로 서비스 시작 순서 제어
- User Secrets를 통한 비밀 정보 관리
- Aspire Dashboard를 통한 분산 추적 및 모니터링

#### 구성 요소

- **`03-apphost.cs`**: 앱 호스트 - 전체 애플리케이션 오케스트레이터
- **`03-minapi.cs`**: Minimal API - Redis에서 메시지를 읽어 반환하는 웹 API
- **`03-worker.cs`**: Background Worker - 1초마다 Redis에 현재 시간을 업데이트
- **`03-apphost.json`**: 앱 호스트 설정 파일

**실행 방법:**

```bash
# 실행 권한 부여
chmod +x 03-apphost.cs

# 실행
./03-apphost.cs
```

**특징:**

- Aspire Dashboard가 자동으로 실행됩니다 (`http://localhost:18888`)
- Garnet (Redis 호환) 캐시 서버가 자동으로 시작됩니다
- Worker가 1초마다 LastUpdated 키를 업데이트합니다
- Minimal API가 메시지와 마지막 업데이트 시간을 반환합니다

**테스트:**

```bash
# Aspire Dashboard에서 minapi의 포트 확인 후
curl http://localhost:<minapi-port>/
```

**아키텍처 흐름:**

```text
┌─────────────┐      ┌─────────────┐      ┌─────────────┐
│   Worker    │─────>│   Garnet    │<─────│   MinAPI    │
│ (03-worker) │ write│  (Redis)    │ read │ (03-minapi) │
└─────────────┘      └─────────────┘      └─────────────┘
      ↓                                           ↓
  매 1초마다                               HTTP 요청 시
 LastUpdated 업데이트                    메시지 조회 및 반환
```

---

### 04. Avalonia GUI

> 🎓 **학습 목표**: FBA에서 크로스 플랫폼 GUI 애플리케이션 작성, MVVM 패턴 적용

Avalonia UI를 사용한 크로스 플랫폼 데스크톱 계산기 애플리케이션입니다. WPF와 유사한 XAML 기반 UI를 코드로 구현한 예제입니다.

**핵심 개념:**

- `#:property OutputType=WinExe` - Windows 실행 파일로 빌드
- Avalonia UI 프레임워크
- MVVM (Model-View-ViewModel) 패턴
- CommunityToolkit.Mvvm을 활용한 관찰 가능 속성
- RelayCommand를 통한 커맨드 패턴
- Dependency Injection과 서비스 생명주기
- 코드 기반 UI 구성 (Code-behind UI)

#### `04-avalonia.cs`

**실행 방법:**

```bash
# 실행 권한 부여
chmod +x 04-avalonia.cs

# 실행
./04-avalonia.cs
```

**특징:**

- CommunityToolkit.Mvvm을 사용한 MVVM 패턴 구현
- 기본 사칙연산 기능 (+, -, ×, ÷)
- 크로스 플랫폼 지원 (Windows, macOS, Linux)
- Dependency Injection을 활용한 서비스 구조

---

### 05. Native AOT

> 🎓 **학습 목표**: Native AOT 컴파일을 통한 네이티브 라이브러리 생성, 다른 언어와의 상호운용성

Native AOT 컴파일을 통해 C# 코드를 네이티브 라이브러리로 변환하고 다양한 언어(Python, C)에서 호출하는 예제입니다. C#의 성능과 다른 언어의 유연성을 결합하는 방법을 보여줍니다.

**핵심 개념:**

- `#:property PublishAot=True` - Native AOT 컴파일 활성화
- `#:property OutputType=Library` - 라이브러리로 빌드
- `#:property NativeLib=Static` - 정적 라이브러리 생성
- `#:property RuntimeIdentifier=osx-arm64` - 타겟 플랫폼 지정
- `UnmanagedCallersOnly` 속성 - 네이티브 함수 노출
- P/Invoke 역방향 (C#에서 네이티브로)
- 언어 간 상호운용성 (C# ↔ Python/C)
- 유니코드 문자열 마샬링
- iconv를 통한 UTF-8 ↔ UTF-16 변환

#### 예제 파일

- **`05-native-aot.cs`**: C# 네이티브 동적 라이브러리 소스 (.dylib)
- **`05-native-aot.py`**: Python에서 동적 라이브러리 호출 예제
- **`05-native-aot.java`**: Java에서 동적 라이브러리 호출 예제 (JNA 사용)
- **`05-native-aot.js`**: Node.js에서 동적 라이브러리 호출 예제 (koffi 사용)
- **`05-staticlib-aot.cs`**: C# 네이티브 정적 라이브러리 소스 (.a)
- **`05-staticlib-c.c`**: C에서 동적 라이브러리 호출 예제

#### 동적 라이브러리 (.dylib) 예제

**빌드 방법:**

```bash
# C# 코드를 네이티브 동적 라이브러리로 컴파일
dotnet publish ./05-native-aot.cs
```

**Python에서 사용:**

```bash
# 실행 권한 부여
chmod +x 05-native-aot.py

# 실행
./05-native-aot.py
```

**C에서 사용:**

```bash
# 컴파일 및 실행
gcc -o 05-staticlib-c 05-staticlib-c.c artifacts/05-native-aot/05-native-aot.dylib -Wl,-rpath,artifacts/05-native-aot -liconv
./05-staticlib-c
```

**Java에서 사용:**

```bash
# JNA 라이브러리 다운로드 (최초 1회)
wget -nc https://repo1.maven.org/maven2/net/java/dev/jna/jna/5.14.0/jna-5.14.0.jar

# 컴파일
javac -cp jna-5.14.0.jar 05-native-aot.java

# 실행
java -cp .:jna-5.14.0.jar NativeAotExample
```

**Node.js에서 사용:**

```bash
# koffi 패키지 설치 (최초 1회)
npm install koffi

# 실행 권한 부여
chmod +x 05-native-aot.js

# 실행
./05-native-aot.js
# 또는
node 05-native-aot.js
```

**출력 예시:**

```text
Testing Native AOT library from Node.js
==========================================

2025. 11. 26. 오후 11:56:52 Hello from Node.js!
2025. 11. 26. 오후 11:56:52 Native AOT 호출 테스트
2025. 11. 26. 오후 11:56:52 안녕하세요!

All calls completed.
```

#### 정적 라이브러리 (.a) 예제

**빌드 방법:**

```bash
# C# 코드를 네이티브 정적 라이브러리로 컴파일
dotnet publish ./05-staticlib-aot.cs
```

> ⚠️ **참고**: 정적 라이브러리는 .NET 런타임 종속성이 포함되지 않아 직접 링크 시 오류가 발생할 수 있습니다. 동적 라이브러리(.dylib) 사용을 권장합니다.

**특징:**

- UnmanagedCallersOnly 속성을 사용한 네이티브 함수 노출
- Python ctypes, Java JNA, Node.js koffi, C에서 C# 라이브러리 호출
- UTF-16 유니코드 문자열 전달 지원 (한글 포함)
- iconv 라이브러리를 통한 간단한 문자 인코딩 변환 (C)
- Java JNA와 Node.js koffi를 통한 간편한 네이티브 라이브러리 접근
- JIT 컴파일러 없이 빠른 시작 시간
- 작은 배포 크기 (단일 네이티브 라이브러리)
- .NET 런타임 불필요 (self-contained)

**사용 사례:**

- Python/Java/Node.js/C 프로젝트에 고성능 C# 라이브러리 통합
- 레거시 시스템과의 연동
- 엣지 디바이스용 경량 라이브러리
- 게임 엔진 플러그인
- 임베디드 시스템용 네이티브 모듈
- JVM 기반 애플리케이션에서 .NET 코드 활용
- Node.js 백엔드에서 고성능 .NET 모듈 활용

---

### 06. AG-UI (AI Agents)

> 🎓 **학습 목표**: AI 에이전트 구축, 커스텀 도구 통합, 스트리밍 응답 처리

Microsoft Agents AI 프레임워크를 사용한 AI 에이전트 서버/클라이언트 예제입니다. OpenAI 호환 API를 통해 Grok 모델을 활용하며, 커스텀 도구(Function Calling)를 지원합니다.

**핵심 개념:**

- Microsoft.Agents.AI 프레임워크
- AG-UI (Agent Gateway User Interface) 프로토콜
- OpenAI 호환 API 사용 (OpenRouter를 통한 Grok 접근)
- Function Calling / Tool Use 패턴
- AIFunctionFactory를 통한 함수 등록
- 스트리밍 응답 처리
- User Secrets를 통한 API 키 관리
- IChatClient 인터페이스 활용

- **`06-agui-server.cs`**: AG-UI 서버 - AI 에이전트 호스팅
- **`06-agui-client.cs`**: AG-UI 클라이언트 - 에이전트와 대화

**서버 설정:**

```bash
# OpenRouter API 키 설정
dotnet user-secrets --id 06-agui-server set openrouter-key "YOUR_API_KEY_HERE"
```

**서버 실행:**

```bash
# 실행 권한 부여
chmod +x 06-agui-server.cs

# 실행
./06-agui-server.cs
```

**클라이언트 실행:**

```bash
# 새 터미널에서
chmod +x 06-agui-client.cs

# 실행
./06-agui-client.cs
```

**특징:**

- OpenAI 호환 API를 통한 Grok 4.1 모델 사용
- 커스텀 도구(Tools) 지원 (덧셈, 뺄셈)
- 스트리밍 응답 지원
- 대화형 CLI 인터페이스

**사용 예시:**

```text
User (:q or quit to exit): 25 더하기 17은?
[Run Started - Thread: xxx, Run: yyy]
25 더하기 17은 42입니다.
[Run Finished - Thread: xxx]

User (:q or quit to exit): :q
```

---

### 07. Blazor WebAssembly

> 🎓 **학습 목표**: FBA에서 Blazor WebAssembly 애플리케이션 작성, 클라이언트 사이드 SPA 구축

Blazor WebAssembly를 사용한 클라이언트 사이드 단일 페이지 애플리케이션(SPA) 예제입니다. 브라우저에서 직접 실행되는 .NET 기반 웹 애플리케이션을 FBA 형태로 작성하는 방법을 보여줍니다.

**핵심 개념:**

- `#:sdk Microsoft.NET.Sdk.BlazorWebAssembly` - Blazor WebAssembly SDK 사용
- `#:property OverrideHtmlAssetPlaceholders=true` - HTML 자산 플레이스홀더 재정의
- `#:property RootNamespace=app` - 루트 네임스페이스 지정
- Razor 컴포넌트 기반 UI 구성
- 라우팅 및 레이아웃 시스템
- HttpClient를 통한 데이터 페칭
- MD2RazorGenerator를 활용한 Markdown 페이지 지원

#### 구성 요소

- **`07-blazor-wasm/app.cs`**: 애플리케이션 진입점 - WebAssemblyHostBuilder 설정
- **`07-blazor-wasm/App.razor`**: 루트 컴포넌트 - 라우터 구성
- **`07-blazor-wasm/_Imports.razor`**: 전역 using 선언
- **`07-blazor-wasm/Layout/`**: 레이아웃 컴포넌트 (MainLayout, NavMenu)
- **`07-blazor-wasm/Pages/`**: 페이지 컴포넌트 (Home, Counter, Weather)
- **`07-blazor-wasm/wwwroot/`**: 정적 자산 (CSS, 샘플 데이터)

#### 프로젝트 구조

```text
07-blazor-wasm/
├── app.cs                 # 진입점 (FBA 메인 파일)
├── App.razor              # 루트 컴포넌트
├── _Imports.razor         # 전역 using
├── Layout/
│   ├── MainLayout.razor   # 메인 레이아웃
│   └── NavMenu.razor      # 네비게이션 메뉴
├── Pages/
│   ├── Home.md            # 홈 페이지 (Markdown)
│   ├── Counter.razor      # 카운터 예제
│   ├── Weather.razor      # 날씨 데이터 페칭 예제
│   └── NotFound.razor     # 404 페이지
└── wwwroot/
    ├── index.html         # HTML 호스트 페이지
    ├── css/               # 스타일시트
    └── sample-data/       # JSON 샘플 데이터
```

**실행 방법:**

```bash
# 07-blazor-wasm 폴더로 이동
cd 07-blazor-wasm

# 실행 (--no-cache 옵션 필수)
dotnet run --no-cache app.cs
```

> ⚠️ **중요**: Blazor WebAssembly FBA는 여러 Razor 파일과 정적 자산을 포함하므로, 변경 사항이 올바르게 반영되려면 반드시 `--no-cache` 옵션을 사용해야 합니다. 이 옵션 없이 실행하면 이전 캐시된 빌드가 사용되어 최신 변경 사항이 적용되지 않을 수 있습니다.

**특징:**

- 브라우저에서 직접 실행되는 .NET 코드 (WebAssembly)
- 서버 없이 정적 호스팅 가능 (GitHub Pages, Azure Static Web Apps 등)
- Bootstrap 기반 반응형 UI
- Markdown 파일을 페이지로 변환 (MD2RazorGenerator)
- 클라이언트 사이드 라우팅
- HttpClient를 통한 JSON 데이터 로딩

**페이지 설명:**

| 페이지 | 경로 | 설명 |
|--------|------|------|
| Home | `/`, `/home` | Markdown으로 작성된 홈 페이지 |
| Counter | `/counter` | 버튼 클릭 카운터 예제 |
| Weather | `/weather` | JSON 파일에서 날씨 데이터 로딩 |

**네임스페이스 변경 시 주의사항:**

네임스페이스를 변경하려면 다음 파일들을 함께 수정해야 합니다:

1. `app.cs`: using 문의 네임스페이스
2. `_Imports.razor`: @using 지시문의 네임스페이스
3. `wwwroot/index.html`: `<namespace>.styles.css` 참조

변경 후 반드시 `dotnet run --no-cache app.cs`로 실행하세요.

**아키텍처:**

```text
┌─────────────────────────────────────────────────────────┐
│                      Browser                            │
│  ┌───────────────────────────────────────────────────┐  │
│  │              Blazor WebAssembly App               │  │
│  │  ┌─────────┐  ┌─────────┐  ┌─────────────────┐   │  │
│  │  │ Router  │──│ Layout  │──│     Pages       │   │  │
│  │  └─────────┘  └─────────┘  │ - Home.md       │   │  │
│  │       │            │       │ - Counter.razor │   │  │
│  │       │            │       │ - Weather.razor │   │  │
│  │       ▼            ▼       └─────────────────┘   │  │
│  │  ┌─────────────────────────────┐                 │  │
│  │  │     .NET WebAssembly        │                 │  │
│  │  │   (dotnet.native.wasm)      │                 │  │
│  │  └─────────────────────────────┘                 │  │
│  └───────────────────────────────────────────────────┘  │
│                         │                               │
│                         ▼                               │
│  ┌───────────────────────────────────────────────────┐  │
│  │              wwwroot (Static Assets)              │  │
│  │         index.html, CSS, sample-data/             │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

---

## 📖 FBA 디렉티브 가이드

FBA에서 사용 가능한 주요 디렉티브들입니다:

### SDK 지정

```csharp
#:sdk Microsoft.NET.Sdk           // 기본 콘솔 애플리케이션
#:sdk Microsoft.NET.Sdk.Web       // 웹 애플리케이션
#:sdk Microsoft.NET.Sdk.Worker    // Worker 서비스
#:sdk Microsoft.NET.Sdk.BlazorWebAssembly  // Blazor WebAssembly
#:sdk Aspire.AppHost.Sdk@13.0.0   // Aspire AppHost (버전 지정)
```

### 패키지 참조

```csharp
#:package Newtonsoft.Json                    // 최신 버전
#:package Newtonsoft.Json@13.0.3             // 특정 버전
#:package Microsoft.Extensions.Hosting@10.*  // 와일드카드 버전
```

### 빌드 속성

```csharp
#:property OutputType=Library                // 라이브러리로 빌드
#:property OutputType=WinExe                 // Windows 실행 파일
#:property PublishAot=true                   // Native AOT 활성화
#:property PublishAot=false                  // Native AOT 비활성화
#:property RuntimeIdentifier=osx-arm64       // 타겟 플랫폼 지정
#:property TargetFramework=net10.0           // 타겟 프레임워크
```

### Shebang 스타일

```csharp
#!/usr/bin/env dotnet              // 표준 방식
///usr/bin/env dotnet "$0" "$@" ; exit $?  // JBang 스타일
```

---

## 🔧 문제 해결

### 실행 권한 오류

**문제:** `Permission denied` 오류 발생

**해결:**

```bash
chmod +x <파일명>.cs
```

### 패키지 복원 오류

**문제:** 패키지를 찾을 수 없거나 버전 충돌

**해결:**

```bash
# NuGet 캐시 정리
dotnet nuget locals all --clear

# 다시 실행
./<파일명>.cs
```

### Native AOT 빌드 실패

**문제:** Native AOT 컴파일 중 오류 발생

**해결:**

- 플랫폼에 맞는 빌드 도구 설치 필요
- **macOS:** Xcode Command Line Tools (`xcode-select --install`)
- **Linux:** GCC/Clang 및 개발 라이브러리
- **Windows:** Visual Studio Build Tools

### Aspire Dashboard 접근 불가

**문제:** Dashboard에 연결할 수 없음

**해결:**

```bash
# 포트 충돌 확인
lsof -i :18888

# 03-apphost.json에서 포트 변경 가능
```

### OpenRouter API 키 오류

**문제:** `openrouter-key is missing` 오류

**해결:**

```bash
# User Secrets 설정 확인
dotnet user-secrets list --id 06-agui-server

# 다시 설정
dotnet user-secrets --id 06-agui-server set openrouter-key "your-api-key"
```

---

## 🤝 기여하기

이 프로젝트에 기여하고 싶으신가요? 환영합니다!

1. 이 저장소를 Fork 하세요
2. Feature 브랜치를 생성하세요 (`git checkout -b feature/AmazingFeature`)
3. 변경사항을 커밋하세요 (`git commit -m 'Add some AmazingFeature'`)
4. 브랜치에 Push 하세요 (`git push origin feature/AmazingFeature`)
5. Pull Request를 열어주세요

**기여 아이디어:**

- 새로운 예제 추가 (gRPC, SignalR, Blazor 등)
- 기존 예제 개선 및 버그 수정
- 문서 번역 (영어, 일본어 등)
- 성능 최적화
- 테스트 코드 추가

---

## 📄 라이선스

이 프로젝트는 MIT 라이선스 하에 배포됩니다.

```text
MIT License

Copyright (c) 2025

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 🔗 참고 자료

### 공식 문서

- [.NET File-Based Applications](https://learn.microsoft.com/dotnet/core/tutorials/top-level-templates) - 공식 FBA 문서
- [.NET 10 Release Notes](https://github.com/dotnet/core/tree/main/release-notes/10.0) - 최신 기능 및 변경사항
- [.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire/) - 클라우드 네이티브 앱 구축
- [Avalonia UI Documentation](https://docs.avaloniaui.net/) - 크로스 플랫폼 UI 프레임워크
- [Native AOT Deployment](https://learn.microsoft.com/dotnet/core/deploying/native-aot/) - 네이티브 컴파일 가이드

### 프레임워크 및 도구

- [Microsoft Agents AI](https://github.com/microsoft/Agents) - AI 에이전트 프레임워크
- [OpenRouter](https://openrouter.ai/) - 통합 LLM API 게이트웨이
- [Garnet](https://microsoft.github.io/garnet/) - Microsoft의 Redis 호환 캐시
- [LINQPad](https://www.linqpad.net/) - .NET 개발 및 쿼리 도구

### 커뮤니티

- [.NET Community](https://dotnet.microsoft.com/platform/community) - 공식 커뮤니티
- [Avalonia Community](https://avaloniaui.net/community) - Avalonia 커뮤니티
- [r/dotnet](https://reddit.com/r/dotnet) - Reddit 커뮤니티

### 관련 프로젝트

- [dotnet/runtime](https://github.com/dotnet/runtime) - .NET 런타임 소스코드
- [dotnet/aspire](https://github.com/dotnet/aspire) - .NET Aspire 소스코드
- [AvaloniaUI/Avalonia](https://github.com/AvaloniaUI/Avalonia) - Avalonia UI 소스코드

---

**⭐ 이 프로젝트가 도움이 되셨다면 Star를 눌러주세요!**

**💬 질문이나 제안사항이 있으시면 Issue를 생성해 주세요.**
