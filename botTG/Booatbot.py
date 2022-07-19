import requests
import datetime
import re

class Bot:
    def __init__(self, token):
        self.token = token
        self.api_url = "https://api.telegram.org/bot{}/".format(token)
        self.offset = 0
        self.func_dict = {
                'text' :  self.reply_to_text_message
                }
        self.familiar_phrases_reply = {
            "hi+\.?|при+в\w*\.?":"poshel nahuy",
            "сам иди*\.?|нет ты*\.?":"net ti",
            "н(е|и)кит пидор*\.?":"da"
            }
        
    def listener(self):
        self.last_update = self.get_last_update()
        print(self.last_update)
        if self.last_update!=None:
            self.input_message_treatment()
            self.offset = self.last_update['update_id'] + 1
            
    def get_updates(self):
        method = 'getUpdates'
        params = {'timeout': 30, 'offset': self.offset}
        resp = requests.get(self.api_url + method, params)
        result_json = resp.json()['result']
        return result_json

    def send_message(self, text):
        if (self.last_update['message']['chat']['type'] == 'group' and 'entities' in self.last_update['message']) or self.last_update['message']['chat']['type'] == 'private':
            params = {'chat_id': self.last_update['message']['chat']['id'], 'text': text}
            method = 'sendMessage'
            resp = requests.post(self.api_url + method, params)
            return resp
    
    def get_last_update(self):
        get_result = self.get_updates()
        if bool(get_result):
            if len(get_result) > 0:
                last_update = get_result[-1]
            else:
                last_update = get_result[len(get_result)]
        else:
            last_update = None
        return last_update
    
    def input_message_treatment(self):
        try:
            for key in self.func_dict.keys():
                if key in self.last_update['message']:
                    print('debug', 1)
                    self.func_dict[key]()
        except KeyError:
            params = {'chat_id': self.last_update['channel_post']['chat']['id'], 'text': 'fuck you indian dancer'}
            method = 'sendMessage'
            requests.post(self.api_url + method, params)
                
    def reply_to_text_message(self):
        temp = re.sub('@booatbot ','',self.last_update['message']['text'].lower())
        for key in self.familiar_phrases_reply.keys():
            if re.match(key, temp):
                print('debug', 2)
                self.send_message(self.familiar_phrases_reply[key])