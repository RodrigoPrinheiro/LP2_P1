# IMDB the DATABASE

**O código para este projeto pode ser encontrado 
[neste repositório público](https://github.com/RodrigoPrinheiro/LP2_P1).**

## Autoria

* ### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488

  * Atualização e manutenção do relatório.
  * Criou e geriu as classes e estruturas responsáveis a guardar os sets de dados 
necessários para criar a base de dados.
  * Implementou na classe `ConsoleInterface` a usabilidade
com pessoas e episódios.
  * Classe de base de dados e procura de informação.

* ### [Tomás Franco](https://github.com/ThomasFranque) 21803301

  * Atualização e manutenção do relatório.
  * Criou as classes responsáveis por ler os ficheiros e cortar as `string` lidas
devidamente, preparando-as para guardar na base de dados.
  * Criou as classes e interfaces de visualização do programa no ecrã.
  * Criou a solução para UI no unity (faltou ligar a base de dados por uns erros
de leitura).

* Todas as escolhas de execução do projeto foram discutidas, planeadas e
pensadas pelos dois elementos do grupo.
* Na visualização dos _commits_ realizados pelo grupo à de ter em atenção o
tamanho da Doxyfile.

## Arquitetura da solução

### Descrição da solução

* A procura e identificação dos títulos está baseado em uma classe `Title`
, esta classe tem todos os membros possíveis de pesquisar por, contendo uma 
`struct` para o _rating_ e duas enumerações para os tipos e géneros possíveis:

* Enumeração `TitleTypes` contem o tipo do título 
(imagem referência da [procura avançada do IMDB]), esta é preenchida adequadamente
mesmo não sendo exatamente a informação em `string` que o ficheiro apresenta.  
![titleTypes]

* Enumeração `TitleGenre` contem o género do título e esta é preenchida
da mesma forma que a `TitleTypes` com o detalhe de esta ser do tipo `[Flags]`
para um título poder conter mais que um género.

* A classe implemente um _composite design pattern_ modificado para o que é
necessário nesta solução.

* O programa ao inicializar vai fazer as seguintes operações antes de deixar o
utilizador fazer uma pesquisa:
  1. Ler Ficheiros menos completos
  2. Guardar informação lida em dicionários adequados com a chave de ID.
  3. Retirar o resto da informação do ficheiro `title.basics` e preencher
a classe `Database.cs` com todos os títulos lidos.
  4. Após retirar esta informação vai criar as relações de episódio entre séries.
* Agora o utilizador pode fazer as perguntas que quiser à base de dados assim como
livremente ver a descrição em detalhe de todos os títulos apresentados.

### Design de Classes

#### Principais relações e responsabilidade das classes

A base de dados, como pode ser visto no diagrama, funciona à base de 3 classes
principais, uma `struct`, e duas enumerações.

##### Funcionamento do programa

* A classe que lê a informação e a distribuí na base de dados é a classe
`DataReader`, assim que esta recolhe informação suficiente dos ficheiros para
"montar" um título este é colocado na coleção principal de `Database`.

* `Database` assim tem uma instância privada de `DataReader` que lhe fornece
a informação necessária. Esta classe funciona a partir de uma coleção genérica
`ICollection<T>`, esta por sua vez contento `Title` com a informação toda que
um título possa ter. `Database` além de guardar a informação também contém
todos os métodos de pergunta de dados (pesquisa por nome, tipo, data, etc...).

* `Title` é a estrutura de dados usada para guardar a informação de um único
título. Os títulos implementam por sua vez um _Composite Design Pattern_ adaptado
para guardar os episódios correspondentes, estes por sua vez também são títulos
com a sua respetiva informação. A classe `Title` é usada pela `DataReader`
e é parte-todo da classe `Database`

##### _Output_ na consola

O _output_ da informação funciona à base de uma classe `ConsoleInterface`
e duas interfaces `IReadable` e `IInterface`

* `IReadable` oferece a uma classe que a implementa funcionalidade para ser escrita
na consola. No nosso caso apenas as classes `Title` e `Person` necessitam desta implementação

* `IInterface` oferece a uma classe a funcionalidade para imprimir a interface
do utilizador no ecrã, necessita de `IReadable`. A classe que implementa esta
interface é a `ConsoleInterface`.

* Por fim `ConsoleInterface` é onde estão todos os métodos e informação para
imprimir no ecrã a base de dados corretamente. Esta funciona à base de constantes
e métodos privados, sendo apenas os públicos os de `IInterface`.

Esta solução foi pensada de forma a que se `IReadable` e `IInterface` fossem
removidos por completo, teríamos apenas a lógica do funcionamento, sem qualquer
parte visual. Dado que com a implementação das mesmas podemos ver no UML o
isolamento da lógica que as mesmas criam.

Pelo facto de ter sido realizada desta forma, colocamos um [*branch*](https://github.com/RodrigoPrinheiro/LP2_P1/tree/unityImplementation) no
repositório remoto com uma solução para a implementação da base de dados em unity.

## Problemas que tivemos e possíveis soluções

Identificados só existem dois problemas conhecidos com o programa.

* Um dos problemas é que quando se realiza uma pesquisa e quer-se voltar a trás
depois de selecionar um título, o _output_ da consola não vai atualizar, ficando
a preto com a seta de seleção a funcionar. Este problema tinha a resolução fácil
de atualizar a consola quando se sai de alguma pesquisa, por motivos de tempo e
a complicação de código entrelaçado de `ConsoleInterface` era difícil resolver
todos os sítios onde este problema acontece.

* O segundo problema é o facto do programa ultrapassar os 6gb de memória durante
o seu uso. Este problema era impossível de resolver dado a forma de como
construímos a solução do inicio, no entanto a solução passaria por ler uma primeira
vez os ficheiros para inicializar os tamanhos, assim como saber o que existe dentro
delas. Quando uma pesquisa era efetuada ai líamos a informação nos ficheiros
necessários e descartavamos o resto após a pesquisa estivesse completa.

## Diagrama UML

![diagrama]

## Referências

As referências usadas para a criação deste programa foram as seguintes:

* [Parse de Enumerações].
* [GZipStream].
* [MemoryStream].
* [Remover elementos `null` dentro de uma lista].
* [Retornar `StreamReader` ao inicio]

[titletypes]:Images/Types.png
[procura avançada do IMDB]:https://www.imdb.com/search/title/?ref_=fn_asr_tt
[diagrama]:Images/DiagramaUML.png
[Parse de Enumerações]:https://docs.microsoft.com/en-us/dotnet/api/system.enum.parse?view=netframework-4.8#System_Enum_Parse_System_Type_System_String_System_Boolean_
[GZipStream]:https://docs.microsoft.com/en-us/dotnet/api/system.io.compression.gzipstream?redirectedfrom=MSDN&view=netframework-4.8
[MemoryStream]:https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=netframework-4.8
[Remover elementos `null` dentro de uma lista]:https://stackoverflow.com/questions/3069748/how-to-remove-all-the-null-elements-inside-a-generic-list-in-one-go
[Retornar `StreamReader` ao inicio]:https://stackoverflow.com/questions/2053206/return-streamreader-to-beginning