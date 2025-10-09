# 🚀 LazyVoom

> XAML 계열(WPF / MAUI / WinUI3 / Avalonia / OpenSilver) 경량 View-ViewModel 자동 연결 라이브러리  
> 프레임워크 아님, 당신만의 아키텍처를 만들어보세요

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)


## 💡 왜 LazyVoom?

- View와 ViewModel 연결만 도와줌  
- **가볍고 단순**, 필요한 만큼만 사용  
- 기존 프로젝트에도 **즉시 추가 가능**  
- 다른 라이브러리와 자유롭게 조합  


## 🆚 Prism과 비교

| | Prism | LazyVoom |
|---|---|---|
| **범위** | Navigation, Modules, EventAggregator 등 전체 프레임워크 | View-ViewModel 연결만 |
| **학습 곡선** | 높음 – 전체 시스템 학습 필요 | 낮음 – 필요한 부분만 사용 |
| **유연성** | 정해진 구조를 따라야 함 | 자유롭게 아키텍처 구성 가능 |
| **적용** | 새 프로젝트에 적합 | 기존/새 프로젝트 모두 쉽게 추가 가능 |
| **크기** | 큼 | 작음 (핵심 기능만) |

> LazyVoom은 **프레임워크가 되지 않고, 당신이 만드는 아키텍처의 한 조각**이 되는 것을 목표로 합니다.  


## ✨ 핵심 특징

- `AutoWireViewModel`로 **자동 연결**  
- **커스텀 컨벤션** 지원  
- **명시적 매핑** 가능  
- **DI/IoC 통합** 가능  
- **초경량** – 핵심 기능만


## 🚀 사용법 핵심

```csharp
// ViewModel 연결 설정
Voom.Instance
    .WithContainerResolver(vmType => Activator.CreateInstance(vmType))
    .WithConvention(viewType => Type.GetType(viewType.FullName + "ViewModel"))
    .WithMapping<SpecialView, SpecialViewModel>();
```

```xml
<!--WPF XAML -->
<UserControl voom:ViewModelLocator.AutoWireViewModel="True">
    <Label Text="{Binding Title}" />
</ContentPage>
```
```xml
<!--MAUI XAML -->
<ContentPage voom:ViewModelLocator.AutoWireViewModel="True">
    <Label Text="{Binding Title}" />
</ContentPage>
```
**원리**: 명시적 매핑 → 컨벤션 → 기본 규칙 → DI → DataContext 설정

## 🔍 동작 원리 (간단 흐름)
View에 AutoWireViewModel="True" 설정 시:

```markdown
명시적 매핑?
      │
      ▼
컨벤션 적용?
      │
      ▼
기본 컨벤션 적용?
      │
      ▼
DI / Container로 ViewModel 생성
      │
      ▼
View의 DataContext 설정
```
설명:

1. 명시적 매핑: WithMapping<> 로 지정한 경우
1. 컨벤션 적용: WithConvention() 로 커스텀 규칙 적용
1. 기본 컨벤션: View → ViewModel 이름 규칙 적용
1. DI / Container: 생성자 주입 지원
1. DataContext: View에 ViewModel 연결 완료

## 🏗️ 구조 예시
```
MyApp/
├── Views/
│   └── MainPage.xaml
├── ViewModels/
│   └── MainPageViewModel.cs
├── Models/
└── Services/
```
## 🎯 모범 사례

✅ 앱 시작 시 한 번만 설정
✅ 컨벤션은 일관되게
✅ 명시적 매핑 최소화
✅ DI와 함께 사용

❌ 런타임 중 설정 변경 X
❌ 수동 DataContext 설정 X
❌ 컨벤션 + 매핑 동시 사용 X

## 🙏 감사의 말

Prism의 ViewModelLocator 패턴에서 영감

Caliburn.Micro의 Convention 시스템 참고

"프레임워크 없이 자유롭게" 를 원하는 개발자들을 위해

LazyVoom은 프레임워크가 되려 하지 않습니다.
당신이 만드는 아키텍처의 한 조각이 되고자 합니다.


