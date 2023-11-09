<template>
    <v-app-bar 
        :elevation="3"
        color="teal-darken-4"
    >
        <v-app-bar-title>Курсы валют</v-app-bar-title>
        <v-spacer></v-spacer>
        <v-btn>
                Пользователь
                <template v-slot:append>
                    <v-icon>mdi-account</v-icon>
                </template>
        </v-btn>
        <template v-slot:append>
            <v-btn icon>
                <v-icon>mdi-dots-vertical</v-icon>
            </v-btn>
        </template>
    </v-app-bar>
    <v-container class="fill-height">
        <v-responsive class="align-center text-center fill-height">
            <v-row class="d-flex">
                <v-col cols="6">
                    <v-row>
                        <v-col>
                            <v-card  elevation="16">
                                <v-table
                                    :hover="true"
                                >
                                    <thead>
                                        <tr>
                                            <th class="text-left">
                                            Идентификатор валюты
                                            </th>
                                            <th class="text-left">
                                            Наименование валюты
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr
                                            v-for="item in currencyItems"
                                            :key="item.id"
                                            @click="currencyRowClick(item)"
                                        >
                                            <td class="text-left">{{ item.id }}</td>
                                            <td class="text-left">{{ item.name }}</td>
                                        </tr>
                                    </tbody>
                                </v-table>
                            </v-card>
                        </v-col>
                    </v-row>
                    <v-row class="d-flex align-center justify-center">
                        <v-col cols="6">
                            <v-pagination
                                :length="totalCount"
                                rounded="circle"
                                @update:model-value="updatePageIndex($event)"
                            ></v-pagination>
                        </v-col>
                    </v-row>
                </v-col>
                <v-col cols="6">
                    <v-card
                        v-show="Object.keys(currencyDetails).length === 0"
                        elevation="16"
                        title="Для получения информации по курсу, выберите валюту."
                    ></v-card>
                    <v-card
                        v-show="Object.keys(currencyDetails).length !== 0"
                        elevation="16"
                        title="Информация"
                    >
                        <v-table>
                            <thead>
                                <tr>
                                    <th class="text-left">Наименование валюты</th>
                                    <th class="text-left">Буквенный код</th>
                                    <th class="text-left">Цифровой код</th>
                                    <th class="text-left">Курс</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-left">{{ currencyDetails.name }}</td>
                                    <td class="text-left">{{ currencyDetails.charCode }}</td>
                                    <td class="text-left">{{ currencyDetails.numCode }}</td>
                                    <td class="text-left">{{ currencyDetails.rate }}</td>
                                </tr>
                            </tbody>
                        </v-table>
                    </v-card>
                </v-col>
            </v-row>
        </v-responsive>
    </v-container>
</template>

<script lang="ts">
    export default {
        data() {
            return {
                select: [],
                currencyItems: [],
                currencyDetails: {},
                totalCount: 0,
                pageIndex: 1,
                pageSize: 10,
                selectedClass: 'selectedRow'
            }
        },
        async created() {
            await this.getTokenAsync();
        },
        async mounted() {
            await this.getTotalCountAsync();
            await this.getCurrenciesAsync(this.pageIndex, this.pageSize);
        },
        methods: {
            async getCurrenciesAsync(pageIndex: number, pageSize: number) {
                const token = sessionStorage.getItem('accessToken');
                const response = await fetch(`https://localhost:5001/CurrencyExchangeRate/currencies?pageIndex=${pageIndex}&pageSize=${pageSize}`, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Authorization": "Bearer " + token
                    }
                });
                if (response.ok === true) {
                    const jsonResponse = await response.json();
                    this.currencyItems = jsonResponse;
                } else {
                    console.log('Status:', response.status);
                }
            },
            async getTotalCountAsync() {
                const token = sessionStorage.getItem('accessToken');
                const response = await fetch("https://localhost:5001/CurrencyExchangeRate/totalCount", {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Authorization": "Bearer " + token
                    }
                });
                if (response.ok === true) {
                    const jsonResponse = await response.json();
                    this.totalCount = Math.floor(jsonResponse / 10);
                } else {
                    console.log('Status:', response.status);
                }
            },
            async getTokenAsync() {
                const formData = new FormData();
                formData.append('grant_type', 'password');
                formData.append('username', 'user@test.ru');
                formData.append('password', 'test_321');
                const response = await fetch("https://localhost:5001/token", {
                    method: "POST",
                    headers: {"Accept": "application/json"},
                    body: formData
                });
                const dataResponse = await response.json();
                if(response.ok === true) {
                    sessionStorage.setItem('accessToken', dataResponse.access_token);
                }
            },
            async getCurrencyAsync(currencyId: string) {
                const formData = new FormData();
                formData.append("currencyId", currencyId);
                const token = sessionStorage.getItem('accessToken');
                const response = await fetch(`https://localhost:5001/CurrencyExchangeRate/currency?currencyId=${currencyId}`, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Authorization": "Bearer " + token
                    }
                });
                if (response.ok === true) {
                    const jsonResponse = await response.json();
                    this.currencyDetails = jsonResponse;
                } else {
                    console.log('Status:', response.status);
                }
            },
            currencyRowClick(currency: any){
                this.getCurrencyAsync(currency.id);
            },
            updatePageIndex(pageIndex: number) {
                this.pageIndex = pageIndex;
                this.getCurrenciesAsync(this.pageIndex, this.pageSize);
            },
        }
    }
</script>

<style scoped>
</style>