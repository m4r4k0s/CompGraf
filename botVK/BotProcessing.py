import vk_api
import math
import pandas as pd
import wikipediaapi
import random
import numpy as np
import re
import pymorphy2
morph = pymorphy2.MorphAnalyzer()
from vk_api.utils import get_random_id
from vk_api.longpoll import VkLongPoll, VkEventType


class Bot :   
    
    def __init__(self, token):
        self.session = vk_api.VkApi(token = token) #создаем сессию 
        self.vk = self.session.get_api() 
        self.wiki = wikipediaapi.Wikipedia('ru') #устанавливаем локаль википедии 
        self.RequestDataBase = pd.DataFrame({'Request':[],'Reply':[]}) #создаем таблицу с запросами, на которые бот не смог ответить
        self.event = None
        self.HelloBase = ['Привет!', 'Здравствуй!', 'Хеллоу)', 'Привеееет.', 'Прив)']
        self.firstMessage = """ Я могу ответить на твой вопрос. Чтобы задать вопрос напиши: Расскажи про/о или просто напиши запрос со знаком вопроса. """
        self.ByeBase = ['До встречи !', 'Пока', 'Пок!']
        self.FunctionDict = { #создаем словарь, где ключь - клюечая фраза, а объект - функция ответа
        "расскажи (про|о) .*|что такое .*|.*\?+": self.WikiRequest,
        "при+в\w*\.?|здра+в\.?|ха+й\.?|хел+о+у\.?|здаро+ва*\.?|he+llo\.?|hi+\.?": self.Greeting,
        "по+ка*\.?|по+ки+\.?|проща+й\.?|давай\.?|до (связи\.?|встречи\.?|свидания\.?)":self.Bye,
        #"масса .*":self.Radius_Black_Hole,
        }
    
    def EventSender(self, event): #сендер для ивета(в питоне нормальной инкапсуляции нет, но пусть будет так)
        self.event = event
        print(self.vk.api.messages.getById())

       
    def KeywordSearch(self):
        NoReply = True
        self.event.text = self.event.text.lower() #приводим строку запроса к нижнему регистру
        for key in self.FunctionDict.keys(): #перебираем все возможные ключи, это нужно оптимизировать, но пока хз как
            if (re.fullmatch(key, self.event.text)): #если ключевая фраза есть в запросе, вызываем обработчик
                self.FunctionDict[key](key)
                NoReply = False 
        if NoReply: #если ответ не нашелся запишим запрос в список запросов без ответа
            self.DefoltReport('')
            df = pd.DataFrame({'Request':[str(self.event.text)],'Reply':['NoN']}).append(self.RequestDataBase)
            self.RequestDataBase = df
            
            
    def DefoltReport(self, key): #ключивые фразы не обнаружены -> даем дефолтный ответ
        if self.event.from_user:
            self.vk.messages.send(user_id=self.event.user_id, message='Я тебя не понимаю' ,random_id=get_random_id())
        elif self.event.from_chat:
            self.vk.messages.send(user_id=self.event.chat_id, message='Я тебя не понимаю',random_id=get_random_id())
            
            
    def Greeting(self, key): #приветствие
        if self.event.from_user:
            tmp = np.random.choice(self.HelloBase) + self.firstMessage
            self.vk.messages.send(user_id=self.event.user_id, message=tmp,random_id=get_random_id())
        elif self.event.from_chat:
            self.vk.messages.send(user_id=self.event.chat_id, message=tmp,random_id=get_random_id())
        
        
    def WikiRequest(self, key): #запрос википедии
        #request = self.event.text[self.event.text.rfind(key)+ len(key):] #отрезаем лишнее от запроса
        tmp = re.sub('расскажи (про|о)|что такое|\??',repl='',string=self.event.text)
        print('Debug1 %s' % self.event.text)
        print('Debug %s' % tmp)
        word = morph.parse(tmp)[0]
        try:
            request = word.inflect({'nomn'}).word
        except:
            self.vk.messages.send(user_id=self.event.user_id, message='Это не вопрос!',random_id=get_random_id())
            return;
        print('MORPH = %s' % request)
        page_py = self.wiki.page(request) #запрашиваем страницу вики
        if page_py.exists(): #страница существует
            reply = str(page_py.summary) + '\n' + str(page_py.fullurl) #выкачиваем краткую справки и добавляем ссылку еа полную статью
            if self.event.from_user:
                self.vk.messages.send(user_id=self.event.user_id, message='Вот что я нашёл: \n' + reply,random_id=get_random_id())
            elif self.event.from_chat:
                self.vk.messages.send(user_id=self.event.chat_id, message='Вот что я нашёл: \n' + reply,random_id=get_random_id())
        else : #страница не нашлась
            #добавляем запрос в таблицу
            df = pd.DataFrame({'Request':[str(request)],'Reply':['NoN']}).append(self.RequestDataBase)
            self.RequestDataBase = df
            if self.event.from_user:
                self.vk.messages.send(user_id=self.event.user_id, message='я ничего не нашел' ,random_id=get_random_id())
            elif self.event.from_chat:
                self.vk.messages.send(user_id=self.event.chat_id, message='я ничего не нашел',random_id=get_random_id())
    
    def Bye(self, key): #прощание
        if self.event.from_user:
            self.vk.messages.send(user_id=self.event.user_id, message=np.random.choice(self.ByeBase,size = 1),random_id=get_random_id())
        elif self.event.from_chat:
            self.vk.messages.send(user_id=self.event.chat_id, message=np.random.choice(self.ByeBase, size = 1),random_id=get_random_id())
    
    
        
 
                
                
                
                
                
