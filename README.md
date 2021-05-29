# Результат работы
![demo](https://user-images.githubusercontent.com/28685443/120072815-93c7af80-c0d0-11eb-828b-33a3f465376f.gif)

____
# Небольшая инструкция.
## Изменение свойств оружия выбранного типа. 
Для изменения свойств оружия можно воспользоваться скриптом **Main**.

![image](https://user-images.githubusercontent.com/28685443/120071364-e782ca80-c0c9-11eb-8b02-3fc98c27d30c.png "!") 

Для изменения внешнего вида оружия (при изменении оружия игрока), требуется добавить объект в **Towers** в скрипте **PlayerShotController**, так же по необходимости установить точку создания снаряда. 

![image](https://user-images.githubusercontent.com/28685443/120071796-b6a39500-c0cb-11eb-87e6-a60c8d6f96f5.png)

## Добавление нового типа оружия
Для добавление нового типа оружия требуется добавить название типа в перечисление **WeaponType** в скрипте **Weapon**.

```C#
    public enum WeaponType {
        Shell, 
        MachineGun, 
        Rocket
        // Новый тип оружия
    }
```

После этого добавить обработку кейса с новым типом оружия:

```C#
    private void Fire()
        {
            if(Time.time - _lastShotTime < _def.delayBetweenShots) return; // Если КД еще не прошло 
             
            Projectile p;
            
            var vel = transform.forward * _def.velocity;
            
            switch (_def.type)
            {
                case WeaponType.Shell:...
                case WeaponType.MachineGun:...
                case WeaponType.Rocket:...
                case Новый тип оружия:...
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
```

## Создание противников 
Для создания противников можно наследовать скрипт Enemy, он содержит необходимы методы.

![image](https://user-images.githubusercontent.com/28685443/120072291-1ac75880-c0ce-11eb-8d77-5a52670ff955.png)

Также переопределяя методы и добавляя новые поля можно создать нового уникального противника, например, создание вражеского танка, который способен держать позицию и производить выстрелы.

![2 (1)](https://user-images.githubusercontent.com/28685443/120072493-28c9a900-c0cf-11eb-982f-b604ff294866.gif)

![image](https://user-images.githubusercontent.com/28685443/120072526-4eef4900-c0cf-11eb-9420-3550b71162e4.png)

## Изменение свойств игрока
Так же основные характеристики игрока можно изменить в скрипте **PlayerStateController**

![image](https://user-images.githubusercontent.com/28685443/120072898-e608d080-c0d0-11eb-843e-d3429dc62c34.png)

