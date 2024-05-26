using DialogueCreationKit.DialogueKit.Domain.Contracts;
using DialogueCreationKit.DialogueKit.Domain.Contracts.Services;
using DialogueCreationKit.DialogueKit.Domain.Model.ViewModel;
using DialogueCreationKit.DialogueKit.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();

builder.Services.AddScoped<IDialogueCreationModel, DialogueCreationModel>();
// init services
builder.Services.AddScoped<IDialogueCreationService, DialogueCreationService>();
builder.Services.AddScoped<IDragAndDropService, DragAndDropService>();
builder.Services.AddScoped<IMorphemesService, MorphemesService>();
//init controller
builder.Services.AddScoped<MorphemesController>();
builder.Services.AddScoped<DragAndDropController>();
builder.Services.AddScoped<DialogueCreationController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
