# IMDB the DATABASE

**O código para este projeto pode ser encontrado 
[neste repositório público](https://github.com/RodrigoPrinheiro/LP2_P1).**

## Autoria

* #### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488
* #### [Tomás Franco](https://github.com/ThomasFranque) 21803301


## Arquitetura da solução

**Solução não definitiva, revisão necessária pelos outros membros de grupo.
A complexidade atual é apenas para compreensão entre os dois membros**

### Descrição da solução

* A procura e identificação dos títulos está baseado em uma `struct` `TitleID`
, esta `struct` terá então dois inteiro, um com o identificador do titulo
 outro com o número de votos do título, um `float` para o _average rating_ e
duas enumerações, uma sendo do tipo `Flags`:

    * A primeira enumeração `TitleTypes` contem o tipo do título 
(imagem referência da [procura avançada do IMDB]), esta é preenchida adequadamente
mesmo não sendo exatamente a informação em `string` que o ficheiro apresenta.
![titleTypes]

    * A segunda enumeração `TitleGenre` contem o género do título, podendo ser
usado para pesquisas semelhantes à de tipo e nome, usando o atributo `Flags` para
poder identificar o título com mais do que um género
(imagem referência da [procura avançada do IMDB]).  
![titleGenres]

### Diagrama UML
![diagrama]

## Referências

[titletypes]:Images/TitleTypes.png
[titleGenres]:Images/TitleGenres.png
[procura avançada do IMDB]:https://www.imdb.com/search/title/?ref_=fn_asr_tt
[diagrama]:Images/DiagramaUML.png