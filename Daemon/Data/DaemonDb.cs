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
        yield return new Advantage("Caçador de Criatura 1", 1)
        {
            Description = "Quando o Jogador escolhe este Aprimoramento, seu Personagem possui a \"missão\" de caçar e exterminar algum tipo de criatura especifica (vampiros, dragões, akumas, asuras, etc) por um motivo qualquer (vingança, ódio, dinheiro, juramento). O Personagem passar a vida toda atrás desse objetivo e sempre se aliará a outros caçadores. O Jogador pode escolher também uma raça inteira (humanos, vedaans, nekos, etc) como a criatura caçada, mas nesse caso será necessário uma boa explicação.\r\no Personagem possui Conhecimento sobre o tipo de criatura que caça com 30%, além de saber como encontrá-los, ou mesmo como destrui-los. Também recebe +5% em todos os Testes de Perícia e combate contra essa criatura, acrescidos de um bónus de + 1 no dano."
        };

        yield return new Advantage("Caçador de Criatura 2", 2)
        {
            Description = "Quando o Jogador escolhe este Aprimoramento, seu Personagem possui a \"missão\" de caçar e exterminar algum tipo de criatura especifica (vampiros, dragões, akumas, asuras, etc) por um motivo qualquer (vingança, ódio, dinheiro, juramento). O Personagem passar a vida toda atrás desse objetivo e sempre se aliará a outros caçadores. O Jogador pode escolher também uma raça inteira (humanos, vedaans, nekos, etc) como a criatura caçada, mas nesse caso será necessário uma boa explicação.\r\no Personagem possui conhecimento de determinado tipo de criatura com 45% e conhece outros caçadores como ele. Pode vir a pertencer a um grupo organizado com esse fim. Também recebe +10% em todos os Testes de Perícia e combate contra essa criatura, acrescidos de um bónus de +2 no dano."
        };

        for (int i = 1; i <= 4; i++)
        {
            yield return new Advantage($"Contatos e Aliados ({i})", i)
            {
                Description = $"Conhecer pessoas nos lugares certos, muitas vezes, vale mais do que ter urna bolsa cheia de ouro. Você conhece pessoas que constituem um sistema cujo auxilio pode ser muito útil algum dia. um aliado importante. \r\nVocê conhece {2^(i-1)} aliado(s) importante(s)."
            };
        }

        yield return new Advantage("Foco em Elemento", 1)
        {
            Description = "Você é especialista em um dos cinco elementos mágicos de Lieh. Todas as magias que você fez com esse elemento são consideradas como se possuíssem focus + 1. Esse elemento escolhido, obrigatoriamente, tem de ser o seu elemento principal. O custo da magia, em pontos de magia, não se alteram. "
        };

        yield return new Advantage("Fracionar Venenos", 1)
        {
            Requirements = "Deve possuir pleo menos 30% de graduação mínima na perícia Herbalismo (venenos)",
            Description = "Você é capaz de separar venenos em substâncias inofensivas. Essas substâncias só podem causar mal quando misturadas. Dessa forma, um assassino pode envenenar sua vitima colocando partes distintas do veneno em vários alimentos conswnidos pela vitima. O dano do veneno e os testes que se fazem para resistir a seus efeitos só se processam quando todos os ingredientes fracionados forem misturados. Fracionar venenos exige um teste de Herbalismo (venenos) difícil."
        };

        yield return new Advantage("Guia Espiritual", 3)
        {
            Requirements = "sacerdotes / miko",
            Description = "O sacerdote atraiu o favor de um espírito que o ajuda no desempenho e na execução de suas magias. Cada vez que o sacerdote lança uma magia, ele pode gastar a sua próxima ação invocando seu guia espíritual. O sacerdote faz um teste de Carisma (normal). Se a jogada tem êxito, o sacerdote não conta os pontos de magia usados para lançar aquela magia O Sacerdote pode Invocar o guia espiritual um número de vezes ao dia igual a seu Nível de personagem. Esse aprimoramento pode ser comprado durante o jogo, mas com uma boa explicação de como o sacerdote conseguiu o guia espirirual"
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

        yield return new Advantage("Olhar da Cobra", 1)
        {
            Requirements = "apenas Nagas",
            Description = "A Naga pode prender a atenção de seu inimigo, olhando fIxamente em seus olhos. Depois de estabelecer contato visual ela faz um teste de Carisma contra a Força de Vontade do alvo. Se ela for bem sucedida o alvo fIca imóvel, incapaz de atacar e de se defender. Esse pode por ser usado até três vezes por dia. Criaturas com mais que cinco níveis de personagem são naturalmente imunes."
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

        yield return new Advantage("Repetir Magia", 2)
        {
            Description = "Você pode escolher uma magia para ser repetida no começo da próxima rodada de combate. Não importa onde o mago esteja, o segundo feitiço afeta a mesma área de efeito que o seu primeiro feitiço. Se a magia repetida for utilizada num alvo ela atingirá o alvo novamente, desde que ele esteja a pelo menos 3m (+ 1 m por nível do mago) de sua posição original. Se o alvo afastar-se além dessa distância a magia é perdida. O cuSto para lançar a magia repetida é 50% menor (arredondado para baixo) que a magia original."
        };

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

        yield return new Advantage("Fetiche Mágico 1", -1)
        {
            Description = "Seu Personagem carece de auto-confiança em suas habilidades mágicas. Ele só é capaz de realizar magias adequadamente quando de posse de um objeto especial que o ajuda a canalizar suas energias. Esse objeto pode ser um colar, um ane~ um bastão ou qualquer objeto pequeno ou médio que o Personagem sempre possa carregar. Esse aprimoramento negativo está disponível em duas graduações, de acordo com a gravidade da fraqueza: Sempre que conjurar magias sem o seu fetiche mágico o personagem estará sujeito a um dos seguintes efeitos, à escolha do Mestre: efeitos reduzidos à metade (dano, área de efeito, aumento de dano ... ) ou dobro do gasto de pontos de magia."
        };

        yield return new Advantage("Fetiche Mágico 2", -2)
        {
            Description = "Seu Personagem carece de auto-confiança em suas habilidades mágicas. Ele só é capaz de realizar magias adequadamente quando de posse de um objeto especial que o ajuda a canalizar suas energias. Esse objeto pode ser um colar, um ane~ um bastão ou qualquer objeto pequeno ou médio que o Personagem sempre possa carregar. Esse aprimoramento negativo está disponível em duas graduações, de acordo com a gravidade da fraqueza: Sem o fetiche mágico o personagem é incapaz de lançar magias de qualquer tipo ou espécie."
        };

        yield return new Advantage("Recuperação Mágica Lenta", -1)
        {
            Description = "Usando esse aprimoramento um personagem que tenha poderes mágicos recupera seus pontos de magia no dobro do tempo que normalmente recuperaria: 1 ponto de magia a cada hora e recuperaria apenas metade de seus pontos de magia depois de uma noite de sono."
        };
    }


    public static List<Advantage> Advantages { get; set; }
}
