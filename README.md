
<h1 align="center" name="Título-e-Imagem-de-capa"> :construction:  Touché Report  :construction: </h1>

![index](https://user-images.githubusercontent.com/45635129/207673376-22448568-ee23-4220-9779-6fdd1792f6b8.png)

<p name="Badges"></p>

![Badge em Desenvolvimento1](https://img.shields.io/github/downloads/diegoluanfs/atos/total) ![Badge em Desenvolvimento2](https://img.shields.io/github/languages/count/diegoluanfs/atos) ![Badge em Desenvolvimento3](https://img.shields.io/github/license/diegoluanfs/atos)

<h1>:truck:Tópicos:truck:</h1>

* [Título e Imagem de capa](#Título-e-Imagem-de-capa)
* [Badges](#badges)
* [Resumo](#resumo)
* [Problemática Social](#problematica-social)
* [Descrição do Projeto](#descrição-do-projeto)
* [Status do Projeto](#status-do-Projeto)
* [Demonstração da aplicação e funcionalidades](#funcionalidades-e-demonstração-da-aplicação)
* [Pessoas Contribuidoras](#pessoas-contribuidoras)
* [Licença](#licença)

<h1 name="Resumo">:tada:Resumo:tada:</h1>

<p> Olá, me chamo <a href="https://www.linkedin.com/in/diego-silva-442216199/">Diego</a> e vou apresentar a aplicação Touché Report que tem como função principal, reportar ocorrências policias de forma simultânea ao evento, e indicar onde aconteceram outros crimes. Esse projeto foi desenvolvido como trabalho final da 4ª Academia .Net, ministrado pela empresa <a href="https://www.linkedin.com/company/atos/">Atos</a> em parceria com a <a href="https://www.linkedin.com/school/ufnuniversidadefranciscana/">Universidade Franciscana - UFN</a>. </p>

<h1 name="problematica-social">:bulb:Problemática Social:bulb:</h1>

<p>A população brasileira vem aumentando com o passar dos anos e isso traz diversos impactos sociais, entre eles, podemos destacar o crescimento desordenado da população. Esse crescimento está diretamente relacionado com o aumento das taxas de criminalidade e isso acontece por vários fatores, entre eles o aumento das taxas de desemprego e subempregos. Outro fator que soma no aumento da criminalidade é o aumento da desigualdade de renda, acarretando em um cenário onde pessoas que possuem poderes aquisitivos menores tenham maior possibilidade de praticarem atividades ilícitas, visto que as qualificações exigidas para desempenhar tais atividades são inferiores às necessárias para desempenhar atividades lícitas, em sua maioria. </p>

<p>Para reduzir as taxas de criminalidade temos a atuação da segurança pública, que tem como princípio, garantir a ordem da nação e fornecer a segurança dos seus cidadãos. Para atingir tal objetivo tem-se procurado diversas alternativas, seja investindo em bens físicos, por meio de aquisições tecnológicas. Os investimentos nessa área apresentam excelentes resultados na redução dos índices de violência e criminalidade em diversas partes do mundo, seja com câmeras de vigilância, dispositivos dotados de inteligência artificial, utilização de big data e outras aplicações.</p>

<p>A expansão da internet está fazendo com que a sociedade caminhe em direção ao modelo de Cidades Digitais, onde as pessoas e instituições estão conectadas por uma infraestrutura de comunicação digital. Podemos entender o conceito de Cidades Digitais como o próximo passo para a evolução urbana, pois teremos uma nova forma de distribuição do fluxo de informações da sociedade, e isso faz com que as cidades fiquem cada vez mais "inteligentes".</p>

<p>O cenário atual, onde grande parte da população tem acesso a celulares e dispositivos eletrônicos conectados a internet é propício para o desenvolvimento aplicações multiplataforma e que sejam colaborativos. Pensando nisso, desenvolvemos a ideia do Touché Report, que consiste em uma aplicação de cunho colaborativo, onde as pessoas podem visualizar os crimes que aconteceram na região e também, reportar crimes.</p>

<h1 name="descrição-do-projeto">📖 Descrição do Projeto 📖</h1>

<p>A aplicação serve para reportar crimes de forma simultânea ao acontecimento, ou seja, caso uma pessoa fique sabendo(ou presencie) um roubo, ela poderá reportar o crime e essa marcação ficará explícita no mapa para outros usuários que acessarem. Para isso, vamos utilizar a arquitetura <a href="https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api/">API Rest</a>, utilizando o padrão <a href="https://www.devmedia.com.br/introducao-ao-padrao-mvc/29308">MVC</a>. As tecnologias, linguagens de programação, frameworks estão separar em front-end, back-end e tools, disponibilizados logo abaixo:
  
<h2>:art: Front-End :art:</h2>

:heavy_check_mark: HTML
:heavy_check_mark: CSS
:heavy_check_mark: JavaScript
:heavy_check_mark: Bootstrap

<h2>⚙ Back-End ⚙</h2>

:heavy_check_mark: C#
:heavy_check_mark: Ado.Net

<h2>🛠 Tools 🛠</h2>

:heavy_check_mark: Json
:heavy_check_mark: Sql Server
  
<h1 name="status-do-Projeto">🚀 Status do projeto 🚀</h1>

<p>A primeira versão do projeto já está disponível, com as funcionalidades principais já implementadas e agora o foco está na dilapidação e limpeza do código.</p>

<h1 name="funcionalidades-e-demonstração-da-aplicação">🕶 Demonstração da aplicação e funcionalidades 🕶</h1>

A página index.html faz com que o usuário acesse o sistema, mas não carregue de imediato as marcações do mapa:

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207715173-df001036-6301-4db9-a86f-9a1a41cba5b4.png" width="400" margin="auto">
</p>

<p>Por outro lado, ao clicar quando o usuário clica em 'View Map', são carregados os marcadores que estão no banco de dados e ficam visíveis no mapa:</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207715206-dc17d428-480b-4eb9-b9de-5f9866e78f31.png" width="400" margin="auto">
</p>

<p>Quando o usuário clicar em 'Login', no canto superior direito da tela, ele será direcionado para outra página:

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207715228-896edced-f9e4-4ad4-8d4c-87df73d383c4.png" width="400" margin="auto">
</p>

<p>Na página de login ainda é possível ir para Sign Up, para realizar o cadastro. Vale ressaltar aqui, que todo o cadastro realizado está programado (nessa versão), para acessar com o login estipulado e a senha padrão ('123').</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207717487-9ddae2cd-7f10-43d0-b9cb-021acac66287.png" width="400" margin="auto">
</p>

<p>Ainda partindo da página de login, é possível ir para a página de esqueci minha senha (forgot password):</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207717843-5a10ea1c-afac-4152-89b1-6aa6de12aa4b.png" width="400" margin="auto">
</p>

<p>Depois de realizar login, o usuário será redirecionado para outra página, onde é possível realizar o reporte de ocorrências:</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207718074-2f890799-f517-4ce5-8a09-f9a3b0e52619.png" width="400" margin="auto">
</p>

<p>Caso o usuário resolva voltar a página index, basta ele clicar em 'Logout', no canto superior direito da tela.</p>

:hammer: Funcionalidades do projeto

- `View Map`: Serve para visualizar o mapa com as ocorrências já existentes
- `Sign In`: Utilizado para logar no sistema
- `Sign Up`: Utilizado para realizar o cadastro de acesso ao sistema
- `Sign Out`: Utilizado para o usuário deslogar do sistema
- `Forgot password`: Utilizado para recuperar senha
- `Report `: Funcionalidade que permite ao usuário(logado), realizar o reporte de uma ocorrência

<p>As funcionalidades foram extraídas do diagrama de caso de uso, que está representado abaixo:</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207687654-bef1f8a6-bd7f-4cc0-bc55-8925e95f225a.png" width="400" margin="auto">
</p>

<p>Falando sobre diagramas, para facilitar o entendimento sobre a estrutura das tabelas construídas no Sql Server, foi gerado um diagrama de banco de dados, baseado na estrutura que comporta esse sistema:</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/45635129/207720793-67f94500-288a-487f-9b8b-e689bc3416e7.png" width="400" margin="auto">
</p>

<h1 name="pessoas-contribuidoras">🙋 Pessoas Contribuidoras 🙋</h1>

* <a href="https://www.linkedin.com/in/diego-silva-442216199/">Diego Luan</a>
  
<h1 name="licença">Licença</h1>

<p>Touché Report is licensed as found in the <a href="https://github.com/diegoluanfs/Atos/blob/master/LICENSE.md">LICENSE</a> file.</p>




