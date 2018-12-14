#Harry Tian
import idna,os,sys,pygame,random
pygame.init()

class game():
    """ the game class with its attributes """
    def __init__(self):
        self.screen = pygame.display.set_mode((600,600))
        pygame.display.set_caption("Meme Wars")
        self.player = pygame.image.load("player.png")
        self.background = pygame.image.load("background.png")
        self.screen.blit(self.background,(0,0))
        self.x,self.y = 275,350
        self.screen.blit(self.player,(self.x,self.y))
        self.player_width = self.player.get_size()[0]
        self.player_height = self.player.get_size()[1]

    def text(self,text,size,x,y):
        """ display text at screen """
        font = pygame.font.SysFont("couriernew",size)
        textSurf = font.render(text,True,(255,255,255))
        textRect = textSurf.get_rect()
        textRect.center = (x,y)
        self.screen.blit(textSurf,textRect)
        pygame.display.update()

    def startScreen(self):
        """ start screen of the game """
        self.text("Flight back",70,300,250)
        self.text("You control     this spaceship",20,300,360)
        self.text("Use the direction buttons to move",20,300,410)
        self.text("Dodge the monster!",20,300,460)
        self.text("Press anything to begin",20,300,510)
        self.text("Press ESC to quit",20,300,560)
		
    def moveUp(self):
        self.y-=1
        self.screen.blit(self.player,(self.x,self.y))
        
    def update(self):
        """ updates the screen with every frame """
        self.screen.blit(self.background,(0,0))     

    def move(self,x,y):
        """ enables movement of player through keyboard """
        self.x += x
        self.y += y
        if self.y > 580 and y <= 0:
            self.screen.blit(self.player,(self.x,580))               
        elif self.y < 20 and y >= 0:
            self.screen.blit(self.player,(self.x,10))
        else:
            self.screen.blit(self.player,(self.x,self.y))
        
    
    def bullets(self,x,y,bullet):
        """ enable movement of bullets """
        bullet = pygame.image.load(bullet)
        self.bulletX = x
        self.bulletY = y
        self.bullet_width = bullet.get_size()[0]
        self.bullet_height = bullet.get_size()[1]
        self.screen.blit(bullet,(self.bulletX,self.bulletY))

    def crash(self):
        """ crash function that returns boolean if player collided with bullet """
        if self.x + self.player_width > self.bulletX and self.x < self.bulletX + self.bullet_width :
            if self.y + self.player_height > self.bulletY and self.y < self.bulletY + self.bullet_height :
                return True
        
    def wrap(self):
        if self.x > 580: 
            self.x = -5
        if self.x < -5:
            self.x = 580
            
    def levelUP(self):
        """ returns boolean if player had finished a level """
        if self.bulletY > 1500:
            return True
        
    def overScreen(self):
        """ game over screen of the game """
        self.text("Game Over",70,300,250)
        self.text("Press ENTER to play again",25,300,350)
        self.text("Press ESC to quit",25,300,400)
        self.text("Icons made by Freepik from www.flaticon.com",20,300,450)
        
    def winScreen(self):
        """ screen when player wins """
        self.text("You Win!",70,300,250)
        self.text("Press ENTER to play again",25,300,350)
        self.text("Press ESC to quit",25,300,400)
        self.text("All icons made by Freepik from www.flaticon.com",20,300,450)
        
def bulletList(a,y_low,y_high,x_low,x_high):
    """ function that creates bullet lists for each level """
    L = []
    x = 0
    for i in range(a):
        y = random.randint(y_low,y_high)
        L.append(x)
        L.append(y)
        x += random.randint(x_low,x_high)
    return L

def rain(Game,i,L,level,bullet):
    """ function that calls bullets and makes bullet rain """
    speed = 6 + level
    L[i+1] += speed
    Game.bullets(L[i],L[i+1],bullet)
    
def main():
    # initialize variables
    clock = pygame.time.Clock()
    Game = game()
    speed = 8
    start = True
    gameOver = False
    # the start screen loop
    while start:
        for event in pygame.event.get(): 
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_ESCAPE:
                    pygame.quit()
                    quit()  
                else:
                    start = False           
        Game.startScreen()
    # the game loop
    while not gameOver:
        # have two loops so that when players replay, they start from the
        # outer loop from level 1
        Game = game()
        # create bullets for 5 levels
        L1 = bulletList(20,-300,50,29,31)
        L2 = bulletList(30,-400,50,19,21)
        L3 = bulletList(40,-500,60,14,16)
        L4 = bulletList(50,-600,50,10,13)
        L5 = bulletList(60,-700,50,9,11)
        level = 1
        keys = [False,False,False,False]
        while not gameOver:
            # enable player movement through keyboard
            x,y = 0,0
            for event in pygame.event.get():
                keys = [False,False,False,False]
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_ESCAPE:
                        pygame.quit()
                        quit()             
                    if event.key == pygame.K_LEFT:
                        keys[0] = True
                    if event.key == pygame.K_RIGHT:
                        keys[1] = True
                    if event.key == pygame.K_UP:
                        keys[2] = True
                    if event.key == pygame.K_DOWN:
                        keys[3] = True
                    if event.type == pygame.KEYUP:
                        if event.key == pygame.K_LEFT:
                            keys[0] = False
                        if event.key == pygame.K_RIGHT:
                            keys[1] = False
                        if event.key == pygame.K_UP:
                            keys[2] = False
                        if event.key == pygame.K_DOWN:
                            keys[3] = False
            if keys[0]:
                x -= speed
            if keys[1]:
                x += speed
            if keys[2]:
                y -= speed
            if keys[3]:
                y += speed
            Game.update()
            Game.move(x,y)
            Game.wrap()
            if level == 1:
                #Game.text("level 1",30,100,50)
                for a in range(0,len(L1)-1,2):
                    rain(Game,a,L1,level,"bug.png")
                    if Game.crash():
                        gameOver = True
                        break
                    if Game.levelUP():
                        level = 2
                        break                                
            if level == 2:
                #Game.text("level 2",30,100,50)
                for a in range(0,len(L2)-1,2):
                    rain(Game,a,L2,level,"unhappy-monster-.png")
                    if Game.crash():
                        gameOver = True
                        break
                    if Game.levelUP():
                        level = 3
                        break
            if level == 3:
                #Game.text("level 3",30,100,50)
                for a in range(0,len(L3)-1,2):
                    rain(Game,a,L3,level,"monster.png")
                    if Game.crash():
                        gameOver = True
                        break
                    if Game.levelUP():
                        level = 4
                        break
            if level == 4:
                #Game.text("level 4",30,100,50)
                for a in range(0,len(L4)-1,2):
                    rain(Game,a,L4,level,"kraken.png")
                    if Game.crash():
                        gameOver = True
                        break
                    if Game.levelUP():
                        level = 5
                        break
            if level == 5:
                #Game.text("level 5",30,100,50)
                for a in range(0,len(L5)-1,2):
                    rain(Game,a,L5,level,"swamp-monster.png")
                    if Game.crash():
                        gameOver = True
                        break
                    if Game.levelUP():
                        level = 6
                        break
            if level == 6 or Game.crash():
                gameOver = True
            pygame.display.update()
            clock.tick(40)
        while level == 6 and gameOver:
            Game.winScreen()
            for event in pygame.event.get():
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_RETURN:
                        gameOver = False
                    elif event.key == pygame.K_ESCAPE:
                        pygame.quit()
                        quit()           
        # gameover screen loop   
        while gameOver:
            Game.overScreen()
            for event in pygame.event.get():
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_RETURN:
                        gameOver = False
                    elif event.key == pygame.K_ESCAPE:
                        pygame.quit()
                        quit()
main()

