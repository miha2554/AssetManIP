# AssetManIP
Редактор "активов" предприятия.
"активы" - условно говоря "набор" объектов типа "актив"
каждый элемент "набора" имеет имя (чтобы по меньшей мере различать их)
для активов типа "деньги" точно известна общая сумма.
деньги могут лежать в банке, а значит для такого типа актива должны быть известны имя банка и счёт в нём
кроме того такой актив может быть без банка (т.е. "деньги в кассе") и вообще номинироваться в разной валюте, либо же просто быть в виде каких нибудь "талонов на бензин", но с твёрдым номиналом.
кроме того, могут быть и неденежные активы, стоимость которых не может быть точно выражена оним числом.
они обычно имеют начальную балансовую стоимость, остаточную балансовую стоимость и оценочную стоимость.
кроме того, некоторые виды неденежных активов могут тоже в чём то измеряться, иметь инвентарные номера, еденицы измерения, даты производства и т.п.
Пример набора активов:
на счету № 5 в ЕвроВорБанке 1000 рублей
на счету № 3 во Внешторгабке 5 долларов
В кассе 100 рублей
В кассе талонов на бензин от Аспека на 3000 рублей
Торговое здание по адресу Бассейная-6, год постройки 1970, начальная стоимость 30000 р, остаточная стоимость 5000р, оценочная стоимость 1000000р, инвентарный номер 7
100 килограммов гвоздей 2000 года изготовления, начальная стоимость 1000р, остаточная стоимость 100р, рыночная стоимость 2000р
