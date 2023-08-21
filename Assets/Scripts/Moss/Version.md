# Moss Version

## 0.0.1

- Singleton
- Game
- Test Runner

## 0.0.2

- Logger
- IOC

## 0.0.3

- IInjectable (model service utility)

## 0.0.4

- EventDispatcher
- SceneLoader
- GameConfig

## 0.0.5

- 更新test runner，增加场景中测试

## 0.0.6

- 修改EventScope
- 增加Event的测试

## 0.0.7

- 增加限制接口，通过this调用Game组件

## 0.0.8

- 增加TestRunner窗口，方便一个一个测试

## 0.0.9

- 更新Courier，更一般化
- 修改了EventTest的测试流程，更加符合现在的框架

## 0.0.10

- 修改EventScope，重新改回string索引
- 重新修改TestRunner结构，将ITestUnit脱离所有框架内类
- 更新SceneLoader的加载模式

## 0.0.11

- 增加GameConfig的OnEndGame()

## 0.0.12

- 修改SceneConfigAttribute 的命名空间为Moss
- 为SceneConfig添加Injector.Clear的接口
- 为IModel添加ICanGetModel接口

## 0.0.13

- 增加SceneConfig，将其改为MonoBehaviour
- 增加Command

## 0.1.0

- 大改，增加SceneContext，Container，移除Injector

## 0.2.0

- 移除Entity
- 重做Container，增加Id

## 0.2.1

- 增加ObservableProperty

## 0.3.0

- 重构结构，将Container内分为3个接口为System，Service，State

## 0.3.1

- 增加CoState，使得每个Mono都可以有自己的独立状态

## 0.3.2 

- 增加ContainGUI