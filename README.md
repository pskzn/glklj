# ЖЖ Галковского

Сохранение ЖЖ Дмитрия Евгеньевича Галковского в читаемых форматах.

Готовые PDF можно скачать тут: https://github.com/orloffm/OrlovMikhail.GalkovskyLJ/releases/tag/2.0.
EPUB и MOBI будут доступны после того, как будет исправлен баг в asciidoctor-epub3.

Все записи со всеми изображениями, а также все комментарии автора сохраняются по годам
в отдельные файлы. 

## Структура проекта

1. Специальная программа сохраняет каждый пост в собственном XML-формате
в файл `book\YYYY\NNNN\dump.xml`, где `YYYY` --- год, а `NNNN` --- номер поста.
После скачивания поста анализируется, какие комментарии войдут в книгу,
и все изображения и юзерпики из самого поста и этих комментариев сохраняются рядом.
Изображения --- в папке `book\YYYY\NNNN\Files`, юзерпики --- в общей для
всех постов папке `book\userpics`.
2. Далее отдельная программа на основе `dump.xml` также анализирует список
комментариев, после чего сохраняет текст поста в формате `AsciiDoc`
в файл `book\YYYY\NNNN\fragment.asc`. Эти файлы совместно составляют
целую книгу.

## Как собрать книгу

Сборка книги возможна только под Windows.

### Подготовка

Потребуются Visual Studio и Ruby.

1. Visual Studio.
	1. Скачать установщик Community Edition 2015: https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx.
	2. Запустить его. Выбрать кастомную установку, снять все галочки. (Т.е. Web-компоненты не нужны, нужен только C#.)
1. Сборка проекта.
	1. Клонировать проекты https://github.com/orloffm/OrlovMikhail.Tools и https://github.com/orloffm/OrlovMikhail.LiveJournalGrabber
	в папки того же уровня. Открыть их `src\*.sln`-файлы в VS и последовательно собрать в Release.
	1. Клонировать данный проект в папку на том же уровне, собрать его `src\*.sln`.
2. Ruby.
	1. Скачать Ruby, версию >= 2.2 с http://rubyinstaller.org/downloads/. Возможно, рабочая ссылка --- http://dl.bintray.com/oneclick/rubyinstaller/rubyinstaller-2.2.2.exe.
	2. Запустить установщик. При установке нужно отметить добавление программ в переменную PATH.
	3. Скачать Ruby Dev Kit оттуда же. Опять же, возможно рабочая ссылка --- http://dl.bintray.com/oneclick/rubyinstaller/DevKit-mingw64-32-4.7.2-20130224-1151-sfx.exe.
	4. Запустить скачанное, распаковать в произвольную папку, например, `C:\RubyDevKit`.
3. Настройка Ruby.
	1. Открыть командную строку.
	1. Перейти в эту папку (`cd C:\RubyDevKit`).
	1. Выполнить `ruby dk.rb init`.
	1. Выполнить `ruby dk.rb install`.
	1. Перейти в папку `book` проекта.
	1. Установить bundler: `gem install bundle`.
	1. Установить всё нужное для сбора книг: `bundle install`.
	
### Сборка книги

1. Открыть консоль. Перейти в папку `book` проекта.
2. Запустить сборку: `make`.
4. Взять готовые файлы в папке `book\output`.

### Что ещё можно делать

1. Очистить результаты: `clean`.
2. Очистить результаты для одного года: `clean 2004`.
3. Собрать фрагменты для одного года: `fragmentate 2004`.
4. Подготовить список корневых документов: `prepare`.
5. Проверить собранные PDF: `pdftest`.
6. Собрать конкретную книгу: `build X`.

## Как скачивать очередной пост

Информационно, это не требуется для сборки книги из исходников.

### Подготовка

1. Собрать проект, как указано выше.
2. Купить платный аккаунт в ЖЖ: http://www.livejournal.com/shop/paidaccount.bml.
3. Переключить на страницу выбора стиля журнала,
http://www.livejournal.com/customize/,
ввести в поиске `SimpleXML`, переключиться на этот layer. Его исходный код
приведён в файле [src/Data/layer.txt](meta/LiveJournal/Data/layer.txt)
Теперь ЖЖ будет выдавать специально сформированный XML для вашего журнала.
Возможно выдавать любой журнал в таком XML-виде путём добавления `?style=mine` 
к адресу. Этим мы и пользуемся.
4. Открыть любой пост в ЖЖ. Включить в браузере по F12 консоль разработчика.
Посмотреть, с каким cookie открывается страница.

### Собственно скачивание

1. Открыть консоль.
2. Зайти в папку `book`.
3. Запустить `dumper /url=<адрес поста> /cookie=<cookie>`.
Здесь:
    * Адрес поста --- полный его адрес, например, http://galkovsky.livejournal.com/15915.html.
    * Cookie --- то, что мы скопировали из консоли разработчика. В кавычках
    
Пример.

`dumper /url=http://galkovsky.livejournal.com/15915.html /cookie="__utma=48425145..."`

#### Примечания

1. Можно добавить параметр `/continue`, тогда будут скачаны посты далее.
2. В следующий раз можно указывать только `/url=...`, кука будет запомнена.
3. Программа автоматически вытаскивает год и номер поста из заголовка. Так что 
в результате приведённой команды будет создан файл `book\2004\0061\dump.xml`,
возможные файлы будут сохранены в `book\2004\0061\Files`, а нужные юзерпики ---
в `book\userpics`.
4. Программу можно запускать подряд, в этом случае файл дампа не переписывается,
а дополняется.

### Завершение после скачивания

1. Вернуться на страницу http://www.livejournal.com/customize/ и переключить
журнал обратно в обычную тему.
