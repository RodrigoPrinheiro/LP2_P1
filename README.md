# IMDB the DATABASE

**O c�digo para este projeto pode ser encontrado 
[neste reposit�rio p�blico](https://github.com/RodrigoPrinheiro/LP2_P1).**

## Autoria

* #### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488
* #### [Tom�s Franco](https://github.com/ThomasFranque) 21803301


## Arquitetura da solu��o

**Solu��o n�o definitiva, revis�o necess�ria pelos outros membros de grupo.
A complexidade atual � apenas para compreens�o entre os dois membros**

### Descri��o da solu��o

* A procura e identifica��o dos t�tulos est� baseado em uma `struct` `TitleID`
, esta `struct` ter� ent�o dois inteiro, um com o identificador do titulo
 outro com o n�mero de votos do t�tulo, um `float` para o _average rating_ e
duas enumera��es, uma sendo do tipo `Flags`:

    * A primeira enumera��o `TitleTypes` contem o tipo do t�tulo 
(imagem refer�ncia da [procura avan�ada do IMDB]), esta � preenchida adequadamente
mesmo n�o sendo exatamente a informa��o em `string` que o ficheiro apresenta.
![titleTypes]

    * A segunda enumera��o `TitleGenre` contem o g�nero do t�tulo, podendo ser
usado para pesquisas semelhantes � de tipo e nome, usando o atributo `Flags` para
poder identificar o t�tulo com mais do que um g�nero
(imagem refer�ncia da [procura avan�ada do IMDB]).  
![titleGenres]

### Diagrama UML
![diagrama]

## Refer�ncias

[titletypes]:Images/TitleTypes.png
[titleGenres]:Images/TitleGenres.png
[procura avan�ada do IMDB]:https://www.imdb.com/search/title/?ref_=fn_asr_tt
[diagrama]:Images/DiagramaUML.png