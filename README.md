- Реализовать следующие расширения методы статического класса `Enumerable`:
    - метод `Where` для фильтрации исходной последовательности;
    - метод `Select` для трансформации исходной последовательности;
    - метод ToArray для получения массива на основе содержимого исходной последовательности;
    - метод `OrderBy`, использующий стратегию сортировки по ключу (сортировка по возрастанию); 
    - метод `OrderBy`, использующий стратегию сортировки по ключу, которая позволяет задавать стратегию сравнения ключей; 
    - метод `Cast`, получающий на основе последовательности не типизированных элементов типизированную последовательность, при этом в случае невозможности приведения хотя бы одного элемента в последовательности, выбрасывается исключение `InvalidCastException`;
    - метод `OfType`, получающий на основе последовательности не типизированных элементов типизированную последовательность только тех элементов, которые могут быть успешно преобразованы к указанному типу;
    - метод `All`, определяющий соответствие всех элементов последовательности заданному предикату, в качестве предиката использовать соответствующую версию делегата `Func`;
    - метод `OrderByDescending`, использующий стратегию сортировки по ключу (сортировка по убыванию);
    - метод `OrderByDescending`, использующий стратегию сортировки по ключу (сортировка по убыванию), которая позволяет задавать стратегию сравнения ключей;
    - метод-генератор `Range` последовательности `count` целых чисел, начиная с некоторого целочисленного значения `start`;
    - метод `Reverse` для получения обратного порядка исходной последовательности;
    - метод `Count` для получения количества элементов исходной последовательности, удовлетворяющих заданному предикату, в качестве предиката использовать соответствующую версию делегата `Func`;
    - метод `Count` для получения количества элементов исходной последовательности.
- Проверить работу разработанных методов, используя различные типы данных.
- Добавить в тестовый проект собстенные версии для тестирования разаработанных методов.
- Дописать недостающие тестовые методы.
