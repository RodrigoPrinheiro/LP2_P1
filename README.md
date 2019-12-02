# IMDB the DATABASE

**O código para este projeto pode ser encontrado 
[neste repositório público](https://github.com/RodrigoPrinheiro/LP2_P1).**

## Autoria

* #### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488

  * Atualização e manutenção do relatório.
  * Criou e geriu as classes e estruturas responsáveis a guardar os sets de dados 
necessários para criar a base de dados.

* #### [Tomás Franco](https://github.com/ThomasFranque) 21803301
    
  * Atualização e manutenção do relatório.
  * Criou as classes responsáveis por ler os ficheiros e cortar as `string` lidas
devidamente, preparando-as para guardar na base de dados.

## Arquitetura da solução

### Descrição da solução

* A procura e identificação dos títulos está baseado em uma classe `Title`
, esta classe tem todos os membros possíveis de pesquisar por, contendo uma 
`struct` para o _rating_ e uma enumeração para os tipos possíveis:

    * Enumeração `TitleTypes` contem o tipo do título 
(imagem referência da [procura avançada do IMDB]), esta é preenchida adequadamente
mesmo não sendo exatamente a informação em `string` que o ficheiro apresenta.  
![titleTypes]

* A classe implemente um _composite design pattern_ modificado para o que é
necessário nesta solução, assim como o _iterator pattern_ para iterar sobre 
os possíveis episódios se este for uma série.

* O programa ao inicializar vai fazer as seguintes operações antes de deixar o
utilizador fazer uma pesquisa:
  1. Ler Ficheiros menos completos
  2. Guardar informação lida em dicionários adequados com a chave de ID.
  3. Retirar o resto da informação do ficheiro `title.basics` e preencher
a classe `Database.cs` com todos os títulos lidos.

 

### Diagrama UML
![diagrama]

## Referências

[titletypes]:Images/Types.png
[procura avançada do IMDB]:https://www.imdb.com/search/title/?ref_=fn_asr_tt
[diagrama]:Images/DiagramaUML.png