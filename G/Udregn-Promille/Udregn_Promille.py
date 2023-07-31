from os import system, getcwd, path # Vi importere denne modul til at ryde konsollen for tekst, getcwd bruger vi når vi skal skrive til en fil
from time import sleep # Vi bruger denne modul til at sove et par sekunder hvis brugeren har brug for at læse noget inden vi gå videre
from datetime import datetime # vi importere tid da vi skal bruge det som label når vi gemmer filer og adskiller hvilken tid det er printet
from colorama import Fore,Back,Style,init # Colorama er en modul som gør det muligt at printe til konsollen med farver
init() #Vi fortæller konsollen at vi vil bruge farver

#Den første er en tom str og det er fordi at senere går vi i udgangspunkt at køn er imellem 1 og 2
KØN                 = ((""),("Pige"), ("Dreng")) 

#Forbudte tegn som ikke må skrives af brugeren
FORBUDTE_TEGN = ["!","'","#","¤","%","&","/","(",")","=","?","`","^","*","_","-",";",",",".","<",">","§","½","@","£","$","€","{","}","[","],"""]

#Vores bruger klasse som vi kommer til at bruge til udregningerne
class Bruger(object):
    def __init__(self,fornavn, efternavn, køn, kropsvægt):
        self.fornavn    = fornavn
        self.efternavn  = efternavn
        self.køn        = køn
        self.kropsvægt  = kropsvægt 
    
    # Dette er en funktion som bliver brugt til at return repræsentationen af klassen
    # Det er ikke en funktion som vi vil komme til at bruge i programmet men det er bare en god ting at huske at lave når man arbejder med klasser
    def __repr__(self):
        return str(f"{self.fornavn} {self.efternavn} {str(self.køn)} {str(self.kropsvægt)}")

def gyldigt_input_str(i): # i = input
    #Først checker vi om der er nogle tal i inputtet og derefter om der er nogle forbudte tegn som vi ikke vil have
    #Any funktionen går igennem en liste og returner true eller false om char findes i listen
    if any(char.isdigit() for char in i) or any(char in FORBUDTE_TEGN for char in i)or len(i) < 2:
        return False
    return True

def gyldigt_input_int(i):
    try: #Vi laver en try except funktion her da vi vil se om det er muligt at lavet inputtet om til integer
        int(i)
        return True
    except ValueError: #Vi forventer fejl angående tal og bruger derefter ValueError exception
        return False

def gyldigt_input_float(i):
    try: #Vi laver en try except funktion her da vi vil se om det er muligt at lavet inputtet om til float
        float(i)
        return True
    except ValueError: #Vi forventer fejl angående tal og bruger derefter ValueError exception
        return False


def lav_bruger():
    header_bruger()
    while True: #Vi binder det til en løkke da vi gerne vil have at inputtet er gyldigt
        print() # {0:<15} betyder at vi giver strengen en plads på 15 tegn for formattering
        fornavn = str(input(Back.CYAN + Fore.BLACK + "{0:<15}".format("Fornavn" + Style.RESET_ALL)  + " # "))
        if not gyldigt_input_str(fornavn):
            if not fornavn == "": # Først tjekker vi om brugeren overhovedet har indtastet noget
                print(f"{fornavn} er ikke et gyldigt fornavn")
                sleep(1) # Sov et sekund
                header_bruger() # Vi rydder konsollen med headeren så man ikke kan spamme konsollen
            continue # Gå tilbage i løkken
        # Vi rydder unyttig tekst
        header_bruger() 
        break #Gyldigt input som breaker løkken
    while True: #Vi laver denne løkke så brugeren ikke behøves at indtaste fornavn igen og at den først breaker når det er gyldigt
        print()# {0:<15} betyder at vi giver strengen en plads på 15 tegn for formattering (i venstre side med '<' )
        efternavn = str(input(Back.CYAN + Fore.BLACK + "{0:<15}".format("Efternavn" + Style.RESET_ALL)  + " # "))
        if not gyldigt_input_str(efternavn):
            if not efternavn == "":
                print(f"{efternavn} er ikke et gyldigt efternavn")
                sleep(1) # Sov et sekund
            header_bruger()
            continue
        # Vi rydder unyttig tekst
        header_bruger() 
        break
    
    while True:
        print() #Vi laver det i f strings her da det er meget nemmere at skifte imellem farverne
        print(f"Er du {Fore.CYAN}pige{Style.RESET_ALL} eller {Fore.CYAN}dreng{Style.RESET_ALL}?")
        print(f"indtast {Fore.CYAN}1{Style.RESET_ALL} for at vælge {Fore.CYAN}pige{Style.RESET_ALL}\nindtast {Fore.CYAN}2{Style.RESET_ALL} for at vælge {Fore.CYAN}dreng{Style.RESET_ALL}")
        print()
        køn = str(input(Back.CYAN + Fore.BLACK + "{0:<15}".format("Køn" + Style.RESET_ALL)  + " # "))
            #Check om tallet er gyldigt
        if not gyldigt_input_int(køn):
            if not køn == "": # Først tjekker vi om brugeren overhovedet har indtastet noget
                print("Indtast et gyldigt tal")
                sleep(1) # Sov et sekund
            header_bruger()
            continue
        køn = int(køn) # Lav køn om til integer da vi nu kan være sikker på at brugeren har indtastet et tal
        #Check om det valgte køn er inden for rammerne 1 og 2, altså pige og dreng
        if køn == 1 or køn == 2:
            #Istedet for at gemme et tal som betyder hvilket køn, så laver vi det om til en str
            køn = KØN[køn]
            # Vi rydder unyttig tekst
            header_bruger() 
            break # Koden er nu nået her til og det betyder at værdien brugern har indtastet er et gyldigt køn
        else: #Hvis det ikke er et gyldigt køn gå tilbage
            print("Indtast et gyldigt køn")
            sleep(1) # Sov et sekund
            header_bruger()
            continue

    while True:
        print()
        print(f"Hvad er din {Fore.CYAN}kropsvægt{Style.RESET_ALL}? (i kg) (Uden decimaler)")
        kropsvægt = str(input(Back.CYAN + Fore.BLACK + "{0:<15}".format("Kropsvægt" + Style.RESET_ALL)  + " # "))
        #Check om tallet er gyldigt
        if not gyldigt_input_int(kropsvægt):
            if not kropsvægt == "":     
                print("Indtast et gyldigt tal")
                sleep(1) # Sov et sekund
            header_bruger()
            continue
        kropsvægt = int(kropsvægt) # Lav kropsvægt om til integer da vi nu kan være sikker på at brugeren har indtastet et tal
        # Check om kropsvægten er realistisk
        if kropsvægt == 0 or kropsvægt > 595: #Talltet 585 kg er fra verdens tykkestemand som er i live, hvilket betyder det er et tal vi kan gå ud fra som er realistisk. Tallet må ikke være nul men gerne mere hvis man har lyst til at teste promilleomregneren med babyer
            print("Indtast en gyldigt kropsvægt")
            sleep(1) # Sov et sekund
            header_bruger()
            continue
        # Vi rydder unyttig tekst
        header_bruger() 
        break # Koden er nu nået her til og det betyder at værdien brugern har indtastet er et gyldigt kropsvægt
    
    return Bruger(fornavn, efternavn, køn, kropsvægt)

def ryd_konsol(): # Vi laver denne funktion da det er meget nemmere at læse når vi skal rydde konsollen i koden
    system("cls")

def print_og_gem_til_fil(i): # i er teksten der skal printes og gemmes # vi printer det og gemmer det
    with open(f"{bruger.fornavn} {bruger.efternavn}.txt", "a") as f:
        print(i)
        f.writelines(i + "\n")
    


def udregn_promille(i): # i = antallet af genstande
    #Først åbner vi filen og skriver en dato for udregningen
    with open(f"{bruger.fornavn} {bruger.efternavn}.txt", "a") as f:
        tid = datetime.now()
        f.write(tid.strftime("%X %x") + "\n")
    print(Fore.CYAN, end="")
    print_og_gem_til_fil("{0:^64}".format("-=*=-"))
    print(Fore.WHITE, end="")
    #Først checker vi hvilket køn brugeren har og udregner ud fra det
    if bruger.køn == "Pige":
        udregning = float((i * 12) / (bruger.kropsvægt * float(0.55)))
    else:
        udregning = float((i * 12) / (bruger.kropsvægt * float(0.68)))
    #Omskriv til 2 decimaler
    udregning = round(udregning, 2)
    print_og_gem_til_fil("{0:>30}  {1:<30}".format("Antal Genstand(e) " + str(int(i)), "Udregnet Promille " + str(udregning)))
    print_og_gem_til_fil("")
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:<30}".format(str(int(i)), "Genstande svarer til:", "Total alkohol i gm " + str(i * 12)))
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(1),1)) , " Normal øl", ".")) # tegnet '^' betyder at vi formattere det i midten af 30 tegn
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(1.3),1)) , " Guld øl", "."))
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(1.75),1)) , " Luksus øl", "."))
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(7),1)) , " Flaske vin", "."))
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(0.5),1)) , " Lille glas spiritus", "."))
    print_og_gem_til_fil("{0:<5} {1:<24} | {2:^30}".format(str(round(i / float(1),1)) , " Stort glas spiritus", "."))
    print_og_gem_til_fil("{0:^30}".format("------------------------------------------------------------"))#60

    forbrænding_pr_time = bruger.kropsvægt * float(0.15) #0.15 g forbrænding i timen pr kil
    genstand_pr_time = forbrænding_pr_time / 12 # en genstand er på 12g

    if bruger.køn == "Pige": # Vi fidner promille pr timen ud fra hvilket køn brugeren har
        promille_pr_time = float((float(genstand_pr_time) * 12) / (bruger.kropsvægt * float(0.55)))
    else:
        promille_pr_time = float((float(genstand_pr_time) * 12) / (bruger.kropsvægt * float(0.68)))
    
    #Udregn hvor mange timer der tager for at få en promille på 0
    ny_promille = udregning
    promille_timen = [] # En liste som vi kommer til at have vores data i
    antal_timer = round(ny_promille / promille_pr_time,2) # Vi kan starte med at regne ud hvor lang tid det rent faktisk tager for at promillen er 0
    print_og_gem_til_fil("{0:^64}".format("Det tager " + str(antal_timer) + " timer for at forbrænde en promille på " + str(ny_promille)))
    print_og_gem_til_fil("")
    print_og_gem_til_fil("{0:^30} | {1:^30}".format("Timer" , "Promille"))
    timer = 0
    while True: #Vi laver en løkke som bliver ved med at køre end til vi breaker
        timer += 1 # For hver gang vi går igennem løkken er der gået en time
        ny_promille -= promille_pr_time # Vi minuser den nye promille med hvor meget brugeren forbrænder i timen
        if ny_promille < 0.0: # Hvis den nye promille er under 0 betyder det at vi er færdig
            # Vi gemmer det i en tuple så vi nemmere kan sætte dem i par og printe dataen fra 0 og 1 index
            promille_timen.append((antal_timer,  0.00)) # Vi har forudset hvor mange timer det tog og kan dermed sige at promillen er på 0.0 med de antal timer
            break # Vi breaker nu hvor vi er færdige
        else:
            promille_timen.append((timer,  round(ny_promille,2))) # Hvis ikke den nye promille er under 0 appender vi de timer og den nye promille med 2 decimaler
    # Vi printer timerne og promillerne i en formateret løsniong
    print
    for i in promille_timen:
        print_og_gem_til_fil("{0:^30} | {1:^32}".format(str(i[0]) , str(i[1])))

    print_og_gem_til_fil("{0:^30}".format("------------------------------------------------------------"))#60

    print_og_gem_til_fil("{0:^64}".format("Påvirking på helbred med en promille på " + str(udregning) + ":"))
    print_og_gem_til_fil("")

    #Her skriver vi hvordan forholdene vil være hvis man har den tilsvarende promille
    if udregning <= 0.2:
        print_og_gem_til_fil("{0:^64}".format("Du bliver dårligere til at fokusere."))
        print_og_gem_til_fil("{0:^64}".format("Dine øjne vil have sværere ved at skifte "))
        print_og_gem_til_fil("{0:^64}".format("mellem mørke og lyse omgivelser."))
    elif udregning <= 0.5:
        print_og_gem_til_fil("{0:^64}".format("Din evne til at opfatte situationer samtidig med,"))
        print_og_gem_til_fil("{0:^64}".format("at du udfører præcise bevægelser bliver dårligere,"))
        print_og_gem_til_fil("{0:^64}".format("og dit synsfelt bliver smallere. "))
        print_og_gem_til_fil("{0:^64}".format("Du må heller ikke længere køre bil."))

    elif udregning <= 1.0:
        print_og_gem_til_fil("")
        print_og_gem_til_fil("{0:^64}".format("Du får sværere ved at koncentrere dig "))
        print_og_gem_til_fil("{0:^64}".format("din opmærksomhed falder, du begynder at blive træt."))
        print_og_gem_til_fil("{0:^64}".format("Din balance bliver også dårligere nu."))
    elif udregning <= 1.5:
        print_og_gem_til_fil("{0:^64}".format("Man får udtalt forringet bevægelsesevne og talebesvær."))
        print_og_gem_til_fil("{0:^64}".format("Centralnervesystemet har fået nok."))
    elif udregning <= 2.0:
        print_og_gem_til_fil("{0:^64}".format("Der er tydelige tegn på forgiftning."))
        print_og_gem_til_fil("{0:^64}".format("Selvkontrollen er også forsvundet."))
    elif udregning <= 3.0:
        print_og_gem_til_fil("{0:^64}".format("Man har manglende kontrol med for eksempel urinblæren"))
        print_og_gem_til_fil("{0:^64}".format("og man kan blive bevidstløs."))
    elif udregning <= 4.0:
        print_og_gem_til_fil("{0:^64}".format("Man bliver bevidstløs, og man er i livsfare."))
    elif udregning >= 4.0:
        print_og_gem_til_fil("{0:^64}".format("Du er sandsynligvis bevistløs og i livsfare."))
    print_og_gem_til_fil("")
    print_og_gem_til_fil("{0:^64}".format(""))
    print(Fore.CYAN, end="")
    print_og_gem_til_fil("{0:^64}".format("-=-=-=-=-=-=-=-=*=-=-=-=-=-=-=-=-"))
    print(Fore.WHITE, end="")
    print_og_gem_til_fil("{0:^64}".format("Udskrevet til")) #  Vi fortæller hvor vi har udskrevet filen, primært i den folderen hvor programmet er
    print_og_gem_til_fil("{0:^64}".format(getcwd().split("\\")[-1])) #Med denne funktion får vi working directory altså den mappe som programmet arbejder sig ud fra og splitter alle pathsne og tager den sidste for at få folder navnet
    print_og_gem_til_fil("")
    print_og_gem_til_fil("")
    print_og_gem_til_fil("")
    print_og_gem_til_fil("")
    print_og_gem_til_fil("") # En masse tomme linjer for at lave mellemrum for hvert print

    
def header(): # Vores header i menuen. Grundet til det er i en funktion er at vi komme til at kalde den mere end 1 gang og tager det i funktionen for bedre kode
    ryd_konsol() 
    print(Fore.WHITE)
    print(f"Velkommen {Fore.CYAN} {bruger.fornavn} {bruger.efternavn} {Fore.WHITE}") # f string som gør det muligt for os at skrive variabler i tekst dette er en meget god funktion i python version 3
    print(f"Indtast {Fore.CYAN}'info'{Fore.WHITE} for at vise infomation om brugeren | Indtast {Fore.CYAN}'ryd'{Fore.WHITE} for at rydde konsollen")
    print(f"Indtast {Fore.CYAN}'genstart'{Fore.WHITE} for at genstarte programmet    | indtast {Fore.CYAN}'stop'{Fore.WHITE} for at lukke programmet")
    print(f"Indtast hvor mange {Fore.CYAN}genstande{Fore.WHITE} du har indtaget i tal for at udregne din promille")
        
def header_bruger(): #Vores header når vi opretter brugeren
    ryd_konsol()#Vi rydder konsollen for at fjerne unyttig tekst
    print(f"Velkommen til {Fore.CYAN}alkoholpromille udregneren{Fore.WHITE}")
    print(f"Start med indtaste {Fore.CYAN}navn{Fore.WHITE},{Fore.CYAN}køn{Fore.WHITE}, og {Fore.CYAN}kropsvægt{Fore.WHITE}")

if __name__ == "__main__":
    while True:
        bruger = lav_bruger() #Vi kalder funktionen lav bruger som går igennem alle statements for at sikre en gyldig bruger og returner den bruger til denne variable
        header()#Print header

        while True: # hav det i en løkke så brugeren kan indtaste kommandoer adskillige gange
            print() # Med Back.CYAN gør vi farven bag teksten blå for at fremhæve inputtet
            kommando = str(input(Back.CYAN + Fore.BLACK + "Indtast" + Style.RESET_ALL + " # ")) # Vi tager brugeren indput og behandler det hvis det er et tal men hvis det er teksten 'stop' så stopper vi programmet
            #Hvis brugeren indtaster et tal betyder det antal genstande og hvis det er en str er det en kommando
            if gyldigt_input_float(kommando):
                genstande = float(kommando) # gør genstande til float nu hvor vi er sikre på at det er gyldigt
                if genstande > 50: # Vi vil gerne have at indputtet er så realisktisk som muligt
                    print("Indtast et realistisk tal")
                    continue
                udregn_promille(genstande)  # Vi gør det om til float for at give brugeren mulighed for at indtaste f.eks. en halv genstand
                
            else:
                if kommando == "info": #Med denne kommado kan brugeren se infomation om den givne bruger
                    print()
                    print("{0:<10} {1} {2}".format("Navn",Fore.CYAN +  bruger.fornavn, bruger.efternavn + Fore.WHITE))
                    print("{0:<10} {1}".format("Køn", Fore.CYAN + bruger.køn + Fore.WHITE))
                    print("{0:<10} {1}{2}".format("Kropsvægt", Fore.CYAN + str(bruger.kropsvægt), "kg" + Fore.WHITE))
                    print()
                elif kommando == "ryd": # Hvis brugeren vil rydde konsollen
                    header()
                elif kommando == "stop": 
                    print("Stopper programmet")
                    sleep(1)
                    exit(0) # Vi stopper programmet med code 0 - clear exit

                elif kommando == "genstart":
                    ryd_konsol()
                    break #Vi breaker den indre løkke og går til den ydre for at lave ny bruger
                else:
                    print("Indtast en gyldig kommando eller tal") # Hvis ikke det er nogle af de ovenstående kommandoer så er det et forsøg på at skrive et ikke gyldigt tal