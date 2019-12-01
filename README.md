# IMDB the DATABASE

**O c�digo para este projeto pode ser encontrado 
[neste reposit�rio p�blico](https://github.com/RodrigoPrinheiro/LP2_P1).**

## Autoria

* #### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488

  * Atualiza��o e manuten��o do relat�rio.
  * Criou e geriu as classes e estruturas respons�veis a guardar os sets de dados 
necess�rios para criar a base de dados.
da base de dados.

* #### [Tom�s Franco](https://github.com/ThomasFranque) 21803301
    
  * Atualiza��o e manuten��o do relat�rio.
  * Criou as classes respons�veis por ler os ficheiros e cortar as `string` lidas
devidamente, preparando-as para guardar na base de dados.

## Arquitetura da solu��o

### Descri��o da solu��o

* A procura e identifica��o dos t�tulos est� baseado em uma classe `Title`
, esta classe tem todos os membros poss�veis de pesquisar por, contendo uma 
`struct` para o _rating_ e uma enumera��o para os tipos poss�veis:

    * Enumera��o `TitleTypes` contem o tipo do t�tulo 
(imagem refer�ncia da [procura avan�ada do IMDB]), esta � preenchida adequadamente
mesmo n�o sendo exatamente a informa��o em `string` que o ficheiro apresenta.  
![titleTypes]

* A classe implemente um _composite design pattern_ modificado para o que �
necess�rio nesta solu��o, assim como o _iterator pattern_ para iterar sobre 
os poss�veis epis�dios se este for uma s�rie.

### Diagrama UML
![diagrama]

## Refer�ncias

[titletypes]:Images/Types.png
[procura avan�ada do IMDB]:https://www.imdb.com/search/title/?ref_=fn_asr_tt
[diagrama]:Images/DiagramaUML.png