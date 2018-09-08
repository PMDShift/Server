@echo off
set /p dbVersion="Specify MySQL Server version (8.0): "
set /p username="Specify your MySQL username: "
set /p password="Specify your MySQL password: "
echo Loading database...
"C:\Program Files\MySQL\MySQL Server %dbVersion%\bin\mysql.exe" -u %username% -p%password% < "Content_Data/mdx_schemas.sql"

echo Loading player table into database...
"C:\Program Files\MySQL\MySQL Server %dbVersion%\bin\mysql.exe" -u %username% -p%password% mdx_players < "Content_Data/mdx_players.sql"
echo Table Loaded!

echo Loading data table into database... (this will take a while)
"C:\Program Files\MySQL\MySQL Server %dbVersion%\bin\mysql.exe" -u %username% -p%password% mdx_data < "Content_Data/mdx_data.sql"
echo Table Loaded!

echo All data loaded!
Pause