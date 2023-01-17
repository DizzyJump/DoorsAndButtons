# DoorsAndButtons

Демонстрационный проект на котором я отрабатываю различные идеи связанные с архитектурой и дизайном кода.

В данном проекте я попытался объединить сервисную архитектуру управляющую жизненным циклом игры с ECS подходом к реализации геймплейной логики.
Проект реализован в обвязке из DI фреймворка Zenject.

Интересной особенностью проекта является то, что геймплейная логика не только реализована ECS-way, но так же реализована полностью отвязанной от движка Unity таким образом, что бы была возможность без проблем вынести её на сервер где нет Unity
