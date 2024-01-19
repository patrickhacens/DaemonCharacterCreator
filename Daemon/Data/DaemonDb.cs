using Daemon.Models;

namespace Daemon.Data;

public static class DaemonDb
{

    static DaemonDb()
    {
        Advantages = [];
        Advantages.AddRange(GetAdvantages());
    }

    private static IEnumerable<Advantage> GetAdvantages()
    {

        yield return new Advantage("Ambidestria", 2)
        {
            Description = "O Personagem pode manusear armas e instrumentos tanto com a mão direi!!, quanto com a esquerda, com igual eficiência. Também pode usar duas armas brancas ao mesmo tempo, se forem pequenas o bastante para o uso com uma única mão. \r\nEstão fora dessa categoria arcos, bestas, lanças, a maioria dos machados e martelos e as grandes espadas. A ambidestria afeta apenas Perícias baseadas em Destreza, não em Agilidade: boxe, artes marciais e outras não recebem beneficias pela ambidestria."
        };

        yield return new Advantage("Arma de Família", 1)
        {
            Description = "As armas são parte da família. Personagens com esse Aprimoramento terão uma arma de família de qualidade superior (tipo de arma à sua escolha). Uma arma de qualidade superior oferece sempre +5% em todos os Testes de Ataque OU de Defesa, e +1 no dano final, mas não são consideradas mágicas. O Personagem jamais pode perder essa arma ou será como se tivesse perdido um ente querido."
        };

        yield return new Advantage("Ataque Bruto", 1)
        {
            Description = "Quando você realiza as ações de ataque ou ataque total num combate corpo-a-corpo, pode escolher subtrair até 10% por nível do seu valor de defesa e acrescentar esse valor como bónus de ataque. As alterações nas jogadas de ataque e na defesa duram até a sua próxima ação."
        };

        for (int i = 1; i <= 5; i++)
        {
            yield return new Advantage($"Ataque furtivo Ninja ({i})", i)
            {
                Description = $"Atacando um inimigo incapaz de se defender adequadamente um personagem pode optar por atingir um ponto vital do seu oponente. Esse Aprimoramento é dividido em 5 níveis. Cada nível adiciona {i*2}d6 pontos de dano extras ao dano normal causado pelo personagem."
            };
        }

        yield return new Advantage("Ataque em movimento", 2)
        {
            Description = "Quando usando a ação de ataque com uma arma corpo-a-corpo, você pode se mover tanto antes, quanto depois do ataque, desde que a distância tOtal percorrida não seja maior que a metade de seu AGI em metros. Você não pode usar este aprimoramento enquanto vestir uma armadura pesada. Você precisa se mover pelo menos 1,5m tanto antes quando depois para se beneficiar dos beneficias do Ataque em Movimento."
        };

        yield return new Advantage("Ataque poderoso", 1)
        {
            Description = "O personagem é capaz de realizar ataques excepcionalmente poderosos e brutais, deixando de lado boa parte de sua técnica. Para cada 10% de penalidade que ele dá a si mesmo, ele aumenta o dano resultante de seu ataque em + 1."
        };

        string[] boaFama = ["líder reconhecido de um bairro", "alguns bairros, uma cidade pequena.", "algumas cidades pequenas, uma metrópole.", "algumas metrópoles, um estado ou região."];
        for (int i = 1; i <= 4; i++)
        {
            yield return new Advantage($"Boa fama ({i})", i)
            {
                Description = "Você é famoso por seus feitos entre as pessoas mortais, sendo uma figura de destaque na midia, política, submundo ou algum outro tipo de ambiente mundano, onde todos reconhecem seu nome e ele é freqüentemente um dos assuntos referidos do público. Este tipo de reputação reflete um melhor tratamento por parte dos NPCs, uma série de convites para reuniões e festas, assédio de tas, ou dezenas de pessoas que querem aprender com você. Existe um motivo para que esse aprimoramento tenha quatro níveis e que o seu irmão, o \"Má Fama\" tenha apenas três: as más noócias correm mais rápido.\r\n"
                + boaFama[i-1]
            };
        }

        yield return new Advantage("Caçador de Criatura 1", 1)
        {
            Description = "Quando o Jogador escolhe este Aprimoramento, seu Personagem possui a \"missão\" de caçar e exterminar algum tipo de criatura especifica (vampiros, dragões, akumas, asuras, etc) por um motivo qualquer (vingança, ódio, dinheiro, juramento). O Personagem passar a vida toda atrás desse objetivo e sempre se aliará a outros caçadores. O Jogador pode escolher também uma raça inteira (humanos, vedaans, nekos, etc) como a criatura caçada, mas nesse caso será necessário uma boa explicação.\r\no Personagem possui Conhecimento sobre o tipo de criatura que caça com 30%, além de saber como encontrá-los, ou mesmo como destrui-los. Também recebe +5% em todos os Testes de Perícia e combate contra essa criatura, acrescidos de um bónus de + 1 no dano."
        };

        yield return new Advantage("Caçador de Criatura 2", 2)
        {
            Description = "Quando o Jogador escolhe este Aprimoramento, seu Personagem possui a \"missão\" de caçar e exterminar algum tipo de criatura especifica (vampiros, dragões, akumas, asuras, etc) por um motivo qualquer (vingança, ódio, dinheiro, juramento). O Personagem passar a vida toda atrás desse objetivo e sempre se aliará a outros caçadores. O Jogador pode escolher também uma raça inteira (humanos, vedaans, nekos, etc) como a criatura caçada, mas nesse caso será necessário uma boa explicação.\r\no Personagem possui conhecimento de determinado tipo de criatura com 45% e conhece outros caçadores como ele. Pode vir a pertencer a um grupo organizado com esse fim. Também recebe +10% em todos os Testes de Perícia e combate contra essa criatura, acrescidos de um bónus de +2 no dano."
        };

        yield return new Advantage("Ki terrestre", 1)
        {
            Requirements = "apenas Samurai",
            Description = "O samurai pode adicionar seu bónus de Carisma (x 5%) a sua jogada de ataque, sua defesa ou a algum teste de resistência fisica, mental ou mágica."
        };
        yield return new Advantage("Chocalho da Cascavel", 1)
        {
            Requirements = "apenas Nagas",
            Description = "Duas vezes por dia a Naga pode usar seu chocalho para intimidar seus oponentes. Quando usa dessa habilidade ela ganha um bónus de + 1 O em todos os testes de Intimidação."
        };

        yield return new Advantage("Concentração", 2)
        {
            Description = "O Personagem é capaz de concentrar-se em seus afazeres com extrema facilidade. Faça um Teste de WILL e, se for bem sucedido, tornará a dificuldade do ato que esteja realizando mais fácil (um Teste Difícil torna-se um Teste Normal). Essa habilidade funciona em meio a um combate, desde que você não esteja fazendo ou recebendo qualquer ataque."
        };

        for (int i = 1; i <= 4; i++)
        {
            yield return new Advantage($"Contatos e Aliados ({i})", i)
            {
                Description = $"Conhecer pessoas nos lugares certos, muitas vezes, vale mais do que ter urna bolsa cheia de ouro. Você conhece pessoas que constituem um sistema cujo auxilio pode ser muito útil algum dia. um aliado importante. \r\nVocê conhece {2^(i-1)} aliado(s) importante(s)."
            };
        }

        yield return new Advantage("Encantar arma ancestral", 1)
        {
            Requirements = "apenas Samurai",
            Description = "O samurai consegue canalizar ser Ki através de sua arma (normalmente uma katana ou wakizashi). Apenas armas ancestrais que também são consideradas extensões de sua alma. Quando empunhada por seu dono a arma ganha um bónus de dano igual ao Nível do samurai / 4 (arredondado para baixo)."
        };

        yield return new Advantage("Especialização em Combate", 1)
        {
            Description = "Quando você realiza as ações de ataque ou nataque total num combate corpo-a-corpo, pode escolher subtrair até 10% por Nível do seu valor de ataque e acrescentar esse valor como bónus de esquiva. As alterações nas jogadas de ataque e na defesa duram até a sua próxima ação."
        };

        yield return new Advantage("Foco em arma", 1)
        {
            Description = "Escolha um tipo de arma. Você também pode escolher \"Artes Marciais\" ou qualquer \"manobra de combate\" que seu personagem possua para efeitos desse Aprimoramento. \r\nVocê ganha um bónus de + 10% em todas as jogadas de ataque realizadas com a arma escolhida. Você pode escolher esse Aprimoramento diversas vezes. Seus efeitos não acumulam. Cada vez que você escolher esse talento, ele se aplica a urna nova arma. Sempre que você passa de nível recebe novo bónus de + 10% nessa perícia. "
        };

        yield return new Advantage("Foco em Arma Maior", 1)
        {
            Description = "Escolha um tipo de arma para a qual você já tenha escolhido Foco em Arma. Você também pode escolher \"Artes Marciais\" ou qualquer \"manobra de combate\" que seu personagem possua para efeitos desse Aprimoramento.você ganha um bónus de + 1 0% em todas as jogadas de ataque realizadas com a arma escolhida. Este bónus acumula com outros bónus na jogada de ataque, inclusive o bónus de Foco em Arma Você pode escolher esse talento diversas vezes. Seus efeitos não acumulam. Cada vez que você escolher esse talento, ele se aplica a uma nova arma."
        };

        yield return new Advantage("Foco em Elemento", 1)
        {
            Description = "Você é especialista em um dos cinco elementos mágicos de Lieh. Todas as magias que você fez com esse elemento são consideradas como se possuíssem focus + 1. Esse elemento escolhido, obrigatoriamente, tem de ser o seu elemento principal. O custo da magia, em pontos de magia, não se alteram. "
        };

        yield return new Advantage("Fracionar Venenos", 1)
        {
            Requirements = "Deve possuir pleo menos 30% de graduação mínima na perícia Herbalismo (venenos)",
            Description = "Você é capaz de separar venenos em substâncias inofensivas. Essas substâncias só podem causar mal quando misturadas. Dessa forma, um assassino pode envenenar sua vitima colocando partes distintas do veneno em vários alimentos conswnidos pela vitima. O dano do veneno e os testes que se fazem para resistir a seus efeitos só se processam quando todos os ingredientes fracionados forem misturados. Fracionar venenos exige um teste de Herbalismo (venenos) difícil."
        };

        yield return new Advantage("Golpe Arrasador", 1)
        {
            Requirements = "apenas Samurais",
            Description = "Uma vez por combate, o samurai pode fazer um teste de Artes Marciais contra a perícia equivalente do oponente. Caso seja bem sucedido ele recebe um bônus na sua próxima jogada de ataque igual a diferença que obteve no teste."
        };

        yield return new Advantage("Guia Espiritual", 3)
        {
            Requirements = "sacerdotes / miko",
            Description = "O sacerdote atraiu o favor de um espírito que o ajuda no desempenho e na execução de suas magias. Cada vez que o sacerdote lança uma magia, ele pode gastar a sua próxima ação invocando seu guia espíritual. O sacerdote faz um teste de Carisma (normal). Se a jogada tem êxito, o sacerdote não conta os pontos de magia usados para lançar aquela magia O Sacerdote pode Invocar o guia espiritual um número de vezes ao dia igual a seu Nível de personagem. Esse aprimoramento pode ser comprado durante o jogo, mas com uma boa explicação de como o sacerdote conseguiu o guia espirirual"
        };

        yield return new Advantage("Intimidação do Olhar", 1)
        {
            Requirements = "apenas Samurais",
            Description = "Sempre que você tentar intimidar um oponente olhando em seus olhos, você pode somar o seu Nível de personagem a seu valor da perícia Intimidar. Caso você seja bem sucedido num teste de Intimidar, o oponente intimidado sofre um redutor de 10% em todos os testes que fizer contra você."
        };

        yield return new Advantage("Intimidação Prolongada", 1)
        {
            Description = "É difícil esquecer a sua Intimidação. Caso você seja bem sucedido num teste de Intimidar, o oponente intimidado sofre um redutor de 10% em todos os testes que fizer contra você, por um número de rodadas igual a seu Nível de personagem."
        };

        string[] maestriaArtesMarciais = ["1d4+FR", "1d6+FR", "1d6+1+FR", "2d6+2+FR", "1d10+FR"];
        for (int i = 1; i <= 5; i++)
        {
            yield return new Advantage($"Maestria em Artes Marciais ({i})", i)
            {
                Description = "Seus golpes de artes marciais são devastadores. Seu dano da perícia Artes Marciais é modificado para " + maestriaArtesMarciais[i-1]
            };
        }

        yield return new Advantage("Maestria em Elemento", 1)
        {
            Requirements = "foco em elemento",
            Description = "A sua especialidade e compreensão de um dos cinco elementos mágicos de Lieh tomou-se formidável Todas as magias que você fez com esse elemento são consideradas como se possuíssem focus +3. Esse elemento escolhido, obrigatoriamente, tem de ser o seu elemento principal. O custo da magia, em pontos de magia, não se altera."
        };

        yield return new Advantage("Magia Amigável", 1)
        {
            Description = "As suas magias não causam dano em seus companheiros. Gastando um ponto de magia extra no momento de se lançar a magia ela se toma inofensiva para qualquer criarura que o mago considere como sua amiga ou para qualquer item que o mago considere importante."
        };

        yield return new Advantage("Magia Dissipável", 1)
        {
            Description = "Você pode eliminar os efeitos de magias ou rituais que realizou no momento que desejar, sem custos adicionais em pontos de magia."
        };

        yield return new Advantage("Magia não-Letal", 1)
        {
            Description = " Quando usa uma magia que cause dano por fogo, frio, eletricidade, ácido, som ou luz você pode modificar essa magia para que ela cause dano não-letal ao invés dos efeitos energéticos que normalmente causaria. Uma bola de fogo conjurada por Criar Fogo 6 ainda causaria 6d6 pontos de dano, mas não seria capaz de acender uma única vela. O dano não letal ou dano concussivo é recuperado rapidamente pelo alvo e se seus pontos de vida forem reduzidos a zero ele é apenas nocauteado. O custo, em pontos de magia, da magia modificada é inalterado."
        };

        yield return new Advantage("Mestre", 1)
        {
            Description = "Todo o Personagem teve, em algum momento de sua aprendizado, um Mestre ou Professor que lhe ensinou sobre suas habilidades - especialmente se essas habilidades são muito incomuns. O Mestre do Jogo deve criar toda a história e características do seu mentor, definindo à seu próprio gosto a personalidade, fraquezas, objetivos e problemas que ele enfrenta. Seu Mestre é um NPC que pode ainda estar vivo ou não. Mesmo que seu mestre esteja morto ou muito distante ele ainda pode auxiliá-lo: em momentos de di.6.culdade você pode se lembrar de alguma técnica ou ensinamento que recebeu."
        };

        yield return new Advantage("Mobilidade", 1)
        {
            Description = "Você é especialmente ágil para se mover dentro de um combate sem ser atingido. Enquanto estiver se movendo dentro de uma área de luta (um campo de batalha, por exemplo) sem combater nenhum inimigo em particular você pode adicionar 20% a seu valor de esquiva."
        };

        yield return new Advantage("Noção de Perigo", 2)
        {
            Description = "Você de vez em quando sente um arrepio dizendo que há alguma coisa de errado. Toda vez que a situação envolver um desastre eminente, uma emboscada, ou qualquer perigo do gênero, o Mestre fará em segredo, um Teste de PER Se obtiver um sucesso, ele te dirá que você pressente que alguma coisa está errada. Um aceno critico fará com que o Mestre lhe dê alguns detalhes sobre a natureza do perigo."
        };

        yield return new Advantage("Olhar da Cobra", 1)
        {
            Requirements = "apenas Nagas",
            Description = "A Naga pode prender a atenção de seu inimigo, olhando fIxamente em seus olhos. Depois de estabelecer contato visual ela faz um teste de Carisma contra a Força de Vontade do alvo. Se ela for bem sucedida o alvo fIca imóvel, incapaz de atacar e de se defender. Esse pode por ser usado até três vezes por dia. Criaturas com mais que cinco níveis de personagem são naturalmente imunes."
        };

        yield return new Advantage("Patrono", 2)
        {
            Description = "Um patrono é alguém ou uma organização para qual O Personagem trabalha. Pode ser um governador, um daimyo, uma ordem militar ou religiosa, uma sociedade secreta, etc. O seu patrono fornece, dentro de certo limites, todo o tipo de equipamento, armamento e fInanciamento que o personagem precisar, mas em troca ele deve realizar todas as missões, cumprir ordens e obter todos os resultados que lhe forem solicitados."
        };

        string[] poderesMágicos = ["Aprendiz. 2 pontos de focus e 1 ponto de magia", "Mago. 3 pontos de focus e 2 de magia.", "Mago Pleno. 5 pontos de foeus e 3 de magia", "Mago Experiente. 7 pontos de foeus e 5 de magia."];
        for (int i = 1; i <= 4; i++)
        {
            yield return new Advantage("Poderes Mágicos", i)
            {
                Requirements = "Obrigatório para personagens magos",
                Description = poderesMágicos[i-1]
            };
        }

        yield return new Advantage("Pontos de Magia Extras", 1)
        {
            Description = "Esse aprimoramento permite que você ganhe um ponto de magia extra, por nível",
        };

        string[] pontosHeroicos = ["Corajoso. 1 Ponto Heróico por nível.", "Valoroso. 2 Pontos Heróicos por nível.", "Intrépido. 3 Pontos Heróicos por nível.", "Herói. 4 Pontos Heróicos por nível."];

        for (int i = 1; i <= 4; i++)
        {
            yield return new Advantage("Pontos Heróicos", i)
            {
                Description = pontosHeroicos[i-1]
            };
        }

        yield return new Advantage("Presa Peçonhenta", 1)
        {
            Requirements = "apenas Nagas",
            Description = "Algumas Nagas, mais ligadas a seu passado ancestral conseguem inocular veneno em suas virimas. O veneno tem o mesmo efeito do veneno de uma cobra e a naga é naturalmente imune a seu próprio veneno. Inocular veneno em baralha é algo dificil: precisa da perícia \"Mordida\" (AGI/ AGI). \r\nDano de ld3+FR perfurante. Teste de CON (vítima mordida) contra 4D. Se falhar sofre ld6 de dano (por veneno natural) a cada meia hora, além de uma febre vertiginosa que causa os seguintes redutores: CON -2, FR -3, DEX -1, AGI -3, PER -4. Pode ser detido com soro anti-ofídico adequado. Após curada, a virima recupera os danos normalmente (1 PV por dia). \r\nO uso da perícia Primeiros Socorros estabiliza o paciente por duas horas, mas não recupera pontos de vida ou valores de atributos perdidos. Antídotos e curas mágicas anulam o veneno. Valores de atriburos são recuperados a uma taxa de 1 ponto por dia."
        };

        yield return new Advantage("Recuperação mágica Acelerada", 2)
        {
            Description = "Usando esse aprimoramento um personagem que tenha poderes mágicos recupera seus pontos de magia na metade do tempo que normalmente reeuperaria: 1 ponto de magia a cada 15 minutos e precisaria de apenas 4 horas de sono para ter seus pontos de magia totalmente restaurados."
        };

        string[] recursos = ["renda de até 200 Hsen mensais.", "renda de até 400 Hsen mensais.", "renda de até 800 Hsen mensais.", "renda de até 1600 Hsen mensais.", "renda de até 3200 Hsen mensais."];
        for (int i = 1; i <= 5; i++)
        {
            yield return new Advantage($"Recursos e dinheiro {i}", i)
            {
                Description = "É quanto de dinheiro, jóias e posses seu Personagem conseguiu reunir ao longo de seus anos de vida. Inclui propriedades e outras fontes de renda que levam tempo até serem convertidas em dinheiro. Os valores a seguir são fornecidos em Hsens (moedas de ouro locais). O jogador deve dar uma explicação convincente de como seu personagem conseguiu esses recursos. O Personagem possui ao todo cerca de 50 vezes o valor de sua renda.\r\n"
                + recursos[i-1]
            };
        }

        yield return new Advantage("Repetir Magia", 2)
        {
            Description = "Você pode escolher uma magia para ser repetida no começo da próxima rodada de combate. Não importa onde o mago esteja, o segundo feitiço afeta a mesma área de efeito que o seu primeiro feitiço. Se a magia repetida for utilizada num alvo ela atingirá o alvo novamente, desde que ele esteja a pelo menos 3m (+ 1 m por nível do mago) de sua posição original. Se o alvo afastar-se além dessa distância a magia é perdida. O cuSto para lançar a magia repetida é 50% menor (arredondado para baixo) que a magia original."
        };

        string[] reputacao = [
            "Seu nome já foi famoso. Você teve seu auge. Hoje, porém, já são poucos que o conhecem. A cada pessoa que encontrar, você tem uma chance de 20% dela conhecê-lo.",
            "Seu nome foi sinónimo de terror ou revolução. Sua participação junto à humanidade pode estar (ou vir a ser) citada em livros de história. A cada pessoa que encontrar, você tem uma chance de 50% dela conhecê-lo",
            "Seu nome é um ícone central da sociedade. Seus atos foram responsáveis, em algum aspecto, pelo estado atua! da sociedade mundana em grande parte do mundo. O personagem conquistou uma fama internacional devido à sua participação decisiva junto à história (um alquimista que descobriu a cura para uma doença, um filósofo que fotrnulou uma teoria, um profeta cujos predizeres estavam corretos, um grande general, etc) e até os seres sobrenaturais podem reconhecer seu nome. Biografias são escritas sobre você, convites para entrevistas e festas chegam às toneladas e seu nome é usada sempre precedido dos adjetivos \"Mestre\" e \"Grande\". A cada pessoa que encontrar, você tem uma chance de 90% dela conhecê-lo."];

        for (int i = 2; i <= 4; i++)
        {
            yield return new Advantage($"Reputação ({i})", i)
            {
                Description = "Existe uma diferença fundamental entre Fama e Reputação: um personagem com Fama É freqüentemente reconhecido pela população, pois é um elemento de destaque momentâneo nos núcleos sociais. Entretanto um personagem com Reputação já teve sua época de fama, mas acabou conservando-a até o presente e as pessoas podem vir . a se recordar de seu período de glória no passado. É quase como ser uma própria lenda.\r\n" +
                reputacao[i-2]
            };
        }

        yield return new Advantage("Resistência à Dor", 3)
        {
            Description = "O personagem possui alta resistência à dor, não importa quanto dano físico sofra, ele não sentirá os ferimentos com a mesma intensidade que as demais pessoas sentem, e nem receber penalidades correspondentes por isso. Considere que o personagem mantém-se consciente mesmo quando seus PV s atingirem o valor O, e só irá desmaiar (morrer) quando chegarem a- 5 (à exceção de golpes localizados na cabeça, desferidos para atordoá-lo)."
        };


        for (int i = 1; i <= 5; i++)
        {
            yield return new Advantage($"Resitência à Magia ({i})", i)
            {
                Description = "O personagem possui uma aura de proteção contra magias arcanas. Na verdade, ele não é totalmente imune à Magia, apenas possui uma resistência sobrenatural capaz de diminuir a intensidade dos efeitos causados contra ele (isso pode reduzir o dano que uma magia, mas também diminuirá os bónus que ela pode conceder). Assim, sempre que uma magia for empregada no personagem, reduza seu efeito confotrne o nível de Resistência à Magia que ele possuir. \r\n"
                + $"{i}D de resistência"
            };
        }

        yield return new Advantage("Resistir a Elementos", 1)
        {
            Description = "Uma vez por dia o personagem pode lançar sobre uma proteção de IP contra um tipo de dano elemental. Essa proteção varia de acordo com o nível do samurai. IP 3 para cada nível do samurai até um máximo de IP 30. Esse efeito dura 5 minutos por nível do personagem."
        };

        yield return new Advantage("Sabio", 1)
        {
            Description =  "existe uma Perícia (exceto Perícias de combate) na qual seu Personagem conhece absolutamente tudo o que existe a respeito. Considere que ele tenha 90% nessa Perícia, além de experiência, contatos e conhecimentos relacionados a essa área. Cada Personagem pode escolher este Aprimoramento somente UMA vez e no máximo dois Personagens de cada grupo podem escolhê-lo. Durante o Jogo, o Mestre pode ajudar o Personagem em questões relacionadas a Perícia escolhida, com idéias, sugestões e o melbor proc~dimento a ser tomado."
        };

        yield return new Advantage("Senhor de Prestígio", 1)
        {
            Description = "O senhor ou Daymo do personagem tem grande influência dentro do regime de Shogunato ou dentro de um clã de samurais, e só pelo fato de você estar servindo a este senhor lhe concede um certo prestígio a tratar com outros cidadãos e até com alguns membros de classe social superior à sua."
        };

        yield return new Advantage("Servo comum", 2)
        {
            Description = "Você tem um servo de confiança que por alguma razão escolheu segui-lo por toda a sua vida, servindo-lhe. Este servo é totalmente leal a você e às suas convicções e morreria por você se fosse necessário.\r\n O personagem ainda é responsável pela saúde e pelo bem estar do servo e será responsabilizado legalmente por tudo o que ele fizer."
        };

        yield return new Advantage("Servo samurai", 4)
        {
            Description = "Você tem um servo de confiança que por alguma razão escolheu segui-lo por toda a sua vida, servindo-lhe. Este servo é totalmente leal a você e às suas convicções e morreria por você se fosse necessário.\r\n O personagem ainda é responsável pela saúde e pelo bem estar do servo e será responsabilizado legalmente por tudo o que ele fizer."
        };

        yield return new Advantage("Servo magistrado", 6)
        {
            Description = "Você tem um servo de confiança que por alguma razão escolheu segui-lo por toda a sua vida, servindo-lhe. Este servo é totalmente leal a você e às suas convicções e morreria por você se fosse necessário.\r\n O personagem ainda é responsável pela saúde e pelo bem estar do servo e será responsabilizado legalmente por tudo o que ele fizer."
        };

        yield return new Advantage("Sociedade Secreta 1", 1)
        {
            Requirements = "Obrigatório se deseja pertencer a clã ninja, escola secreta de magia, grupo buddhista/shintoista",
            Description = "Por suas habilidades, você foi escolhido e recrutado por uma sociedade secreta, onde você será treinado e aprenderá a refinar suas aptidões. Ou talvez você não tenha sido recrutado: você simplesmente nasceu dentro desse grupo. Sendo membro de uma Sociedade Secreta, você receberá regalias e inimigos. Existem varias ordens, tanto de magos quanta de samurais, para saber quais os beneficias e maleficios de cada uma consulte o Mestre.\r\n"
            + "Seu Personagem pertence à Sociedade, mas como um membro dos circulas externos, ou aprendizes. A maioria dos Ninjas está nessa categoria."
        };

        yield return new Advantage("Sociedade Secreta 2", 2)
        {
            Requirements = "Obrigatório se deseja pertencer a clã ninja, escola secreta de magia, grupo buddhista/shintoista",
            Description = "Por suas habilidades, você foi escolhido e recrutado por uma sociedade secreta, onde você será treinado e aprenderá a refinar suas aptidões. Ou talvez você não tenha sido recrutado: você simplesmente nasceu dentro desse grupo. Sendo membro de uma Sociedade Secreta, você receberá regalias e inimigos. Existem varias ordens, tanto de magos quanta de samurais, para saber quais os beneficias e maleficios de cada uma consulte o Mestre.\r\n"
            + "Seu Personagem já possui uma influência maior dentro da Sociedade e, assim, pertence aos círculos mais internos dela (possuindo talvez até algum titulo mais importante)."
        };

        string[] status = ["um adivinho, professor ou astrólogo, samurai em começo de carreira.", "um nobre, um samurai respeitado, um sacerdote ou monge.", "um governante de cidade ou um lorde de terras, um daimyo menor.", "um governante de província, um daimyo maior, membro da corte do imperador.", "Imperador."];
        for (int i = 1; i <= 5; i++)
        {
            yield return new Advantage($"Status ({i})", i)
            {
                Description = "Você possui urna certa reputação e uma estabilidade nas comunidades civilizadas. Esse status costuma ser determinado pelo de seu Mestre, e pelo respeito devido à sua linhagem e à sua classe social. Quanto mais Status você possui, melhor será tratado pelos poderosos, e mais respeitado você será. A grande população de Lieh pode ser considerada detentora de Status zero, a não ser que tenham adquirido tanto poder e atenção que precisem ser tratados com respeito. O Status é usado algumas vezes para modificar os testes de que envolvam pericias sociais e reflete o seu prestígio nesses casos.\r\n"
                + status[i-1]
            };
        }

        yield return new Advantage("Temperamento Calmo", 2)
        {
            Description = "O Personagem é uma pessoa com grande conhecimento e controle sobre suas emoções, sendo capaz de medir suas alterações de comportamento. Isso impede que ele fica agitado ou bravo facilmente, bem como se manter tranqüilo frente à um inimigo e não demonstrar medo (embora esteja realmente apavorado). Dobre todos os valores de um Teste quando a situação estiver relacionada à dissimulação ou auto-controle (como Força de Vontade, Carisma, Lábia, ln timidação, Meditação e outros)."
        };
        yield return new Advantage("Tiro Certeiro", 1)
        {
            Description = "Você ganha um bónus de + 1 0% nas jogadas de ataque e + 1 nas jogadas de dano com armas de ataque à distância dentre 9m."
        };

        yield return new Advantage("Tiro em Movimento", 1)
        {
            Requirements = "AGI e DEX maiores que 13 e pelo menos 40% na perícia de arma de alcance",
            Description = "Quando usando a ação de ataque com uma arma de alcance, você pode se mover tanto antes, quanto depois do ataque, desde que a distância total percorrida não seja maior que seu valor de AGI em metros."
        };

        yield return new Advantage("Tiro Longo", 1)
        {
            Requirements = "Aprimoramento \"Tiro Certeiro\"",
            Description = "Quando você usa uma arma de projétil, como um arco, a distância atingida é aumentada em 50% (multiplique por 1,5). Quando você usa uma arma de arremesso, a distância atingida é dobrada."
        };

        yield return new Advantage("Tiro Múltiplo", 2)
        {
            Description = "Como uma ação de combate, você pode atirar duas flechas contra um único oponente até uma distância de 9m. Ambas as flechas usam a mesma jogada de ataque (com uma penalidade de -20%) para determinar o sucesso e causar dano normalmente. Para cada 25% de pericia com arco e flecha que você tiver acima de 35%, você pode adicionar uma flecha a mais neste ataque até um máximo de 4 flechas. Porém, cada flecha depois da segunda adiciona uma penalidade cumulativa na jogada de ataque (com um total de -30% para três flechas e -40% para quatro). Indice de proteção e outras resistências se aplicam separadamente contra cada flecha atirada."
        };

        yield return new Advantage("Tiro Preciso", 1)
        {
            Requirements = "Aprimoramento \"Tiro Certeiro\"",
            Description = "Você pode atirar ou arremessar uma arma de alcance num oponente engajado em combate corpo-a-corpo sem receber a penalidades na sua jogada de ataque."
        };

        yield return new Advantage("Tiro Rápido", 1)
        {
            Description = "Você pode fazer um ataque extra por rodada com uma arma de ataque à distância. Este ataque é feito normalmente, mas cada ataque que fizer nesta rodada (os ataques regulares e o extra) é submetido a um redutor de -10%, cada. Você precisa usar uma ação de rodada completa para utilizar este talento."
        };

        yield return new Advantage("Alcoólatra", -1)
        {
            Description = "O personagem é viciado em álcool, ou uma bebida especifica. Sempre que ele estiver próximo da bebida, o Mestre deve exigir um Teste de WILL caso ele queira evitá-la. Para um personagem bêbado, TODOS os Testes são Difíceis."
        };

        yield return new Advantage("Andrógino", -1)
        {
            Description = "O Personagem tem a aparência tisica trocada, seu corpo tem os traços do sexo oposto, o que faz com que ele seja facilmente confundido com uma mulher (se O personagem for um homem) ou com um homem (se for uma mulher). Terá um corpo truculento, se for mulher, e um corpo frágil se for homem."
        };

        yield return new Advantage("Aparência Trocada", -1)
        {
            Description = "O Personagem é incrivelmente idêntico a uma outra pessoa. Ambos são fisicamente iguais, mas possuem personalidades e comportamento muito diferentes. Mesmo para os amigos mais próximos, é quase impossível distinguir qu~m é quem apenas baseando-se na aparência. Isso pode trazer multos problemas para o Personagem, pois geralmente o seu \"gêmeo\" costuma meter-se em encrencas e você acaba sendo considerado o culpado."
        };

        yield return new Advantage("Armas Ancestrais", -1)
        {
            Description = "O espírito de um guerreiro como O samurai repousa em suas armas ancestrais (no caso do samurai, sua katana). Essas armas são essenciais à seus portadores, como extensões de suas almas. E perdê-las significa perder sua honra, o que muitas vezes resulta em suppuku (suicídio ritual). Um samurai preferirá destruir sua espada do que entregá-la a um inimigo."
        };

        yield return new Advantage("Código de Honra", -1)
        {
            Description = "P personagem segue algum rígido código de conduta e jamais poderá desobedecê-lo, nem mesmo que sua vida dependa disso. Existem muitos códigos de honra, mas para o samurai existe apenas o Bushido, o caminho do guerreiro."
        };

        yield return new Advantage("Compulsão", -1)
        {
            Description = "O personagem sente uma necessidade incontrolável de fazer uma certa tarefa. Se trata de algum tique nervoso, que o personagem repete compulsivamente sem que ele próprio perceba (limpar a arma, lavar as mãos, piscar o olho, pronunciar ao final de cada frase a palavra \"né\", chamar a todos por tio/ tia, etc). Embora pareça uma desvantagem fraca ela pose trazer conseqüências desastrosas, como chamar um poderoso senhor de terras de tio ..."
        };

        yield return new Advantage("Coração Mole", -1)
        {
            Description = "O seu Personagem é muito sentimental e não é capaz de ver ninguém sofrer. Ele acredita que todos, sem distinção, merecem a clemência, o perdão e uma segunda chance. Por isso, ele NUNCA recusa nenhum pedido de ajuda e jamais irá matar ou ferir gravemente seu oponente durante uma batalha, preferindo deixá-lo inconsciente ou até mesmo perder a luta. O sofrimento é algo que ninguém merece."
        };

        yield return new Advantage("Dever", -1)
        {
            Description = "Você deve obediência a uma pessoa, ordem militar, hierárquica, religiosa ou a uma obsessão. Sua vida é dedicada a cumprir seus deveres com essa entidade e nada mais na sua vida importa: nem mesmo sua vida. Um . . personagem com esse apnmoramento negativo raramente afasta-se do seu grande objetivo e quando o faz não consegue se concentrar direito no que esta fazendo. Ele sofre um redutor de -20% em todas a suas perícias. Em adição a isso o personagem não pode desobedecer a entidade a que ele serve. Ele prefere morrer a fazer isso. Se um samurai falha com seu dever, ele prefere morrer do que viver com a vergonha. Caso ele escolha viver com a vergonha ele se torna um Ronin."
        };

        yield return new Advantage("Dificuldade de Fala", -1)
        {
            Description = "O personagem é gago, tem a lingua presa, ou qualquer outro problemas de dicção que dificulta o entendimento do que ele fala pelas outras pessoas. Esse aprimoramento deve ser interpretado à mesa de jogo. "
        };

        yield return new Advantage("Distração", -1)
        {
            Description = "O personagem não consegue se concentrar em nada, vivendo no mundo das nuvens. Todos os Testes de WIIL para se concentrar são Dificeis. Um personagem com Distração não pode comprar O Aprimoramento Concentração."
        };

        yield return new Advantage("Excesso de confiança", -1)
        {
            Description = "Você se enxerga mais forte, sábio e competente do que realmente é. Estará sempre se gabando de sua superioridade e procurando oportunidades para demonstrá-las a todos á sua volta. Sempre que você se envolver em uma situação onde suas habilidades não são capazes de resolver o problema, ou você estiver em desvantagem numérica, só vai abandonar a tarefa ou a luta se for bem sucedido num teste de Força de Vontade (dificil) e mesmo assim vai culpar algum fator externo pelas coisas não terem saído certo."
        };

        yield return new Advantage("Fanático", -2)
        {
            Description = "Sua vida é devotada a algum objetivo específico. O Personagem persegue incessantemente esse objetivo, e todos seus atos são guiados por ele. Toda a vida dele é regida por esta causa e nada o fará desistir dela, pois ele acredita que valha qualquer sacrificio em seu nome. Pode ser fanatismo religioso, nacionalismo fervoroso, a busca incansável por um artefato lendário, uma vingança pessoal."
        };

        yield return new Advantage("Fetiche Mágico 1", -1)
        {
            Description = "Seu Personagem carece de auto-confiança em suas habilidades mágicas. Ele só é capaz de realizar magias adequadamente quando de posse de um objeto especial que o ajuda a canalizar suas energias. Esse objeto pode ser um colar, um ane~ um bastão ou qualquer objeto pequeno ou médio que o Personagem sempre possa carregar. Esse aprimoramento negativo está disponível em duas graduações, de acordo com a gravidade da fraqueza: Sempre que conjurar magias sem o seu fetiche mágico o personagem estará sujeito a um dos seguintes efeitos, à escolha do Mestre: efeitos reduzidos à metade (dano, área de efeito, aumento de dano ... ) ou dobro do gasto de pontos de magia."
        };

        yield return new Advantage("Fetiche Mágico 2", -2)
        {
            Description = "Seu Personagem carece de auto-confiança em suas habilidades mágicas. Ele só é capaz de realizar magias adequadamente quando de posse de um objeto especial que o ajuda a canalizar suas energias. Esse objeto pode ser um colar, um ane~ um bastão ou qualquer objeto pequeno ou médio que o Personagem sempre possa carregar. Esse aprimoramento negativo está disponível em duas graduações, de acordo com a gravidade da fraqueza: Sem o fetiche mágico o personagem é incapaz de lançar magias de qualquer tipo ou espécie."
        };

        yield return new Advantage("Glutão", -1)
        {
            Description = "Você está sempre faminto. Não importa o quanto coma, você sempre terá aquela sensação eminente de fome. Esse aprimoramento é uma compulsão mental. A cada 3 horas você deve se alimentar fartamente ou comer algum tipo de doce. Caso não possa se alimentar deve fazer um Teste de Força de Vontade. Em caso de falha ficará com um redutor de -3 em Força até se alimentar novamente."
        };

        yield return new Advantage("Identidade Secreta", -1)
        {
            Description = "O Personagem com esse aprimoramento negativo não pode revelar a verdadeira natureza de sua existência. Ele deve fazer de tudo para que sua verdadeira identidade não seja revelada. Em outras palavras, ele é forçado a viver uma vida dupla: ele deve aparentar ter uma vida no~ com arrugos, uma residência fixa, um emprego ou profissão comum. Caso seja descoberto, sua vida estará em sério risco. Sempre que ele for agir, deve ter em mente alguma coisa que preserve sua identidade."
        };

        yield return new Advantage("Intrometido", -1)
        {
            Description = "Em Lieh a discrição é uma qualidade muito admirada, especialmente em lugares com rígidas normas sociais como é O caso de Handívia e Ryuujin. Infelizmente você é aquele que se intromete no que não é da sua conta. Sempre que for capaz de ouvir alguma conversa você vai se intrometer na mesma hora e dar sua opirúão sobre o assunto, mesmo que não seja convidado, ou que não seja prudente falar naquele momento. Sempre que você interrompe alguém, atrai sobre você um redutor de -20% em todos os testes sociais. Um personagem pode evitar se intrometer, fazendo um teste de Força de Vontade difícil."
        };

        string[] maFama = ["alguns bairros, uma cidade pequena.", "algumas cidades pequenas, uma metrópole.", "algumas metrópoles, um estado ou região."];
        for (int i = 1; i <= 3; i++)
        {
            yield return new Advantage($"Má fama ({i})", -i)
            {
                Description = "Você é alguém famoso, mas devido aos seus crimes. Basicamente funciona exatamente corno o Aprimoramento Boa Fama, porém de uma maneira negativa, que faz a sociedade considerá-lo uma \"má pessoa\" ou um elemento perigoso, estando sempre sob suspeita e descrença.\r\n"
                + maFama[i-1]
            };
        }

        yield return new Advantage("Marca do Predador", -1)
        {
            Description = "Os animais reconhecem o personagem como uma ameaça, fugindo quando o sentem sua aproximação. Em alguns casos, os animais podem ser mais agressivos e tentar atacá-lo para se protegerem, ou a suas proles."
        };

        yield return new Advantage("Pária", -1)
        {
            Description = "Por algum motivo o personagem é considerado um pária da sociedade. Todos os que vivem naquela região o rejeitam e até o temem. Os camponeses o temem ou porque você quebrou a hierarquia de classes de Lieh, ou os samurais o desprezam porque você é apenas um arremedo mal-feito de ser humano. Todos os testes sociais de um paria são submeridos a um redutor de 20% , desde que o paria seja identificado como tal."
        };

        yield return new Advantage("Recuperação Mágica Lenta", -1)
        {
            Description = "Usando esse aprimoramento um personagem que tenha poderes mágicos recupera seus pontos de magia no dobro do tempo que normalmente recuperaria: 1 ponto de magia a cada hora e recuperaria apenas metade de seus pontos de magia depois de uma noite de sono."
        };

        yield return new Advantage("Senhor Indigno", -1)
        {
            Description = "O senhor ou Daimyo é considerado indigno de confiança, é avarento, ou talvez trate mal os seus servos, mas independente dos motivos ele é detestado por muitos membros do regime de Shogunato ou dentro de um clã de samurais. O fato de você estar servindo a este senhor lhe traz muitos problemas, pois as pessoas tendem a te tratar mal. Ainda assim você segue seu senhor."
        };
    }


    public static List<Advantage> Advantages { get; set; }
}
