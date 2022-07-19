import numpy as np
import pandas as pd 
import math
from scipy.optimize import minimize
from scipy.stats import norm


class Сlassifier:

    regressions = {
        'Logistic': lambda theta, x: 1/(1+np.exp(-x.mul(theta[1:], axis = 1).sum(axis=1)-theta[0])),
        'Cauchy': lambda theta, x: (1/math.pi)*np.arctan(-x.mul(theta[1:], axis = 1).sum(axis=1)-theta[0])+0.5,
        'Probit': lambda theta, x: norm.cdf(-x.mul(theta[1:], axis = 1).sum(axis=1)-theta[0])
    }

    def __init__(self, fit, aim, method = 'Logistic'):
        if not(method in self.regressions):
            raise Exception("invalid method name")
        self.method = method
        self.X = fit
        self.Y = aim
        self.trained = False
        self.predict = None
        self.tht = np.zeros((fit.shape[1]+1, 1))
        
    def Trein(self):
        theta = self.tht.copy()
        x = self.X.copy()
        y = self.Y.copy()
        parent = self
        opt_weights = minimize(self.Error_cost, theta, method='Powell', args=(x, y, parent))
        self.tht = opt_weights['x']
        self.trained = True
        return 0
        
    def Error_cost(self, theta, x, y, parent):
        tmp = parent.regressions[self.method](theta, x)
        cost = -np.sum(y * np.log(tmp) + (1 - y) * np.log(1 - tmp))
        return cost
            
    def Predict(self, fit = None):
        if self.trained:
            if fit is None:
                self.predict = np.array(self.regressions[self.method](self.tht, self.X))
            else:
                return np.array(self.regressions[self.method](self.tht, fit))
            return self.predict
        else:
            self.Trein()
            if fit is None:
                self.predict = np.array(self.regressions[self.method](self.tht, self.X))
            else:
                return np.array(self.regressions[self.method](self.tht, fit))
            return self.predict
        
    def AIC(self):
        if self.predict is None:
            self.Predict()
        L = np.prod((self.predict**self.Y)*(1-self.predict)**(1-self.Y))
        return [2*(self.tht.size-1)-2*np.log(L), L]
    
    def Accuracy(self):
        if self.predict is None:
            self.Predict()
        predicted_classes = (self.predict >= 0.5).astype(int)
        accuracy = np.mean(predicted_classes == self.Y)
        return accuracy
    
    def Replace_data(self, fit, aim):
        self.X = fit
        self.Y = aim
        self.trained = False
        self.predict = None
        self.tht = np.zeros((fit.shape[1]+1, 1))
        return 0
        
    def Replace_method(self, method):
        self.method = method
        self.trained = False
        self.predict = None
        self.tht = np.zeros((self.X.shape[1]+1, 1))
        return 0