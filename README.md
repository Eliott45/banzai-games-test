# Результат работы
![demo](https://user-images.githubusercontent.com/28685443/120071333-c326ee00-c0c9-11eb-88d3-178225a82d8d.gif)
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



