const Routes = {
  home : '',
  userProfile: 'users/profile/:id',
  login: 'login',
  myProfile: 'users/me',
  followers: 'users/followers',
  buildUrl(route: string, ...params: (string | number)[]) {
    const routeParamPlaceholders = route.split('/').filter(p => p.startsWith(':'));

    if (!routeParamPlaceholders.length) {
      return route;
    }

    if (routeParamPlaceholders.length !== params.length) {
      throw Error(`Not all parameters are provided! Route: ${route}`);
    }

    let i = 0;
    return route.replace(new RegExp(routeParamPlaceholders.join('|'), 'gi'), () => params[i++].toString());
  }
};

export { Routes };
